using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorRegistrationSystem.Common.Utility.Results.Types;
using VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs;
using VisitorRegistrationSystem.Repository.IRepository;
using VisitorRegistrationSystem.Services.IServices;

namespace VisitorRegistrationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeparmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DeparmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _departmentService.GetAll();

            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Data);

            return BadRequest(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = await _departmentService.Get(id);

            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Data);

            return BadRequest(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentAddDto departmentAddDto, [FromHeader] string createdByName)
        {
            var result = await _departmentService.Add(departmentAddDto, createdByName);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return CreatedAtAction(nameof(GetDepartmentById), new { id = result.Data.Department.Id }, result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentUpdateDto departmentUpdateDto, [FromHeader] string modifiedByName)
        {
            if (id != departmentUpdateDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _departmentService.Get(id);
            if (result.ResultStatus == ResultStatus.Error)
            {
                return NotFound(result.Message);
            }

            var updateResult = await _departmentService.Update(departmentUpdateDto, modifiedByName);
            if (updateResult.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return BadRequest(updateResult.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id, [FromHeader] string modifiedByName)
        {
            var result = await _departmentService.Delete(id, modifiedByName);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return NotFound(result.Message);
        }

        [HttpPost("HardDelete/{id}")]
        public async Task<IActionResult> HardDeleteDepartment(int id)
        {
            var result = await _departmentService.HardDelete(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return NotFound(result.Message);
        }

        [HttpGet("GetDepartmentUpdateDto/{id}")]
        public async Task<IActionResult> GetDepartmentUpdateDto(int id)
        {
            var result = await _departmentService.GetDepartmentUpdateDto(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }



    }
}
