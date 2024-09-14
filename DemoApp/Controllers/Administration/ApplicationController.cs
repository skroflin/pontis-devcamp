using DemoApp.Core.Dtos.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Core.Utils.Security.Attributes;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Api.Controllers.Administration
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService application)
        {
            _applicationService = application;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetApplicationsPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _applicationService.GetApplicationPaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            var result = await _applicationService.GetApplications();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication([FromRoute] int id)
        {
            var result = await _applicationService.GetApplication(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertApplication([FromBody] ApplicationDto applicationDto)
        {
            await _applicationService.InsertApplication(applicationDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateApplication([FromBody] ApplicationDto applicationDto)
        {
            await _applicationService.UpdateApplication(applicationDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication([FromRoute] int id)
        {
            await _applicationService.DeleteApplication(id);
            return Ok();
        }
    }
}
