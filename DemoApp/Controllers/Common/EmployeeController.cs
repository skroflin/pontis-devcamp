using DemoApp.Core.Dtos.Common;
using DemoApp.Core.Services.Common.Interfaces;
using DemoApp.Core.Utils.Security.Attributes;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.api.Controllers.Common
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    [Authorize("admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employee) 
        {
            _employeeService = employee;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetEmployeesPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _employeeService.GetEmployeePaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _employeeService.GetEmployees();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            var result = await _employeeService.GetEmployee(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee([FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.InsertEmployee(employeeDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.UpdateEmployee(employeeDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            await _employeeService.DeleteEmployee(id);
            return Ok();
        }
    }
}
