using Microsoft.AspNetCore.Mvc;
using VisitorRegistrationSystem.Services.Services;
using VisitorRegistrationSystem.Domain.DTOs.VisitorDTOs;
using System.Threading.Tasks;
using VisitorRegistrationSystem.Common.Utility.Results.Types;
using VisitorRegistrationSystem.Services.IServices;

namespace VisitorRegistrationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorsController : ControllerBase
    {
        private readonly IVisitorService _visitorService;

        public VisitorsController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpGet("GetAllVisitors")]
        public async Task<IActionResult> GetAllVisitors()
        {
            var result = await _visitorService.GetAllNonDeleted();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitorById(int id)
        {
            var result = await _visitorService.Get(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitor([FromBody] VisitorAddDto visitorAddDto, [FromHeader] string createdByName)
        {
            var result = await _visitorService.Add(visitorAddDto, createdByName);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return CreatedAtAction(nameof(GetVisitorById), new { id = result.Data.Visitor.Id }, result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisitor(int id, [FromBody] VisitorUpdateDto visitorUpdateDto, [FromHeader] string modifiedByName)
        {
            if (id != visitorUpdateDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _visitorService.Get(id);
            if (result.ResultStatus == ResultStatus.Error)
            {
                return NotFound(result.Message);
            }

            var updateResult = await _visitorService.Update(visitorUpdateDto, modifiedByName);
            if (updateResult.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return BadRequest(updateResult.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id, [FromHeader] string modifiedByName)
        {
            var result = await _visitorService.Delete(id, modifiedByName);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return NotFound(result.Message);
        }

        [HttpPost("IsExit/{id}")]
        public async Task<IActionResult> VisitorIsExit(int id, [FromHeader] string modifiedByName)
        {
            var result  = await _visitorService.IsExit(id, modifiedByName);

            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
    }
}
