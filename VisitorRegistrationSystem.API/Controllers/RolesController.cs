using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VisitorRegistrationSystem.Common.Utility.Results.Types;
using VisitorRegistrationSystem.Domain.DTOs.RoleDTOs;
using VisitorRegistrationSystem.Domain.Entitiy;
using Microsoft.EntityFrameworkCore;

namespace VisitorRegistrationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet("GetAllRoles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleListDto = new RoleListDto()
            {
                Roles = roles,
                ResultStatus = ResultStatus.Success
            };
            return Ok(roleListDto);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] RoleAddDto roleAddDto)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<Role>(roleAddDto);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(new RoleDto()
                    {
                        ResultStatus = ResultStatus.Success,
                        Message = $"{roleAddDto.Name} yetki adı, başarıyla eklenmiştir.",
                        Role = role
                    });
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("Delete/{roleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return NotFound("Role not found");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok(new RoleDto()
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{role.Name} adındaki yetki, başarıyla silinmiştir.",
                    Role = role
                });
            }
            else
            {
                return BadRequest(new RoleDto()
                {
                    Role = role,
                    ResultStatus = ResultStatus.Error,
                    Message = $"{role.Name} adındaki yetki, silinememiştir."
                });
            }
        }
    }
}
