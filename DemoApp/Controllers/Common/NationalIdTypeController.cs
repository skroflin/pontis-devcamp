using DemoApp.Core.Dtos.Common;
using DemoApp.Core.Services.Common.Interfaces;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.api.Controllers.Common
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class NationalIdTypeController : ControllerBase
    {
        private readonly INationalIdTypeService _nationalIdTypeService;

        public NationalIdTypeController (INationalIdTypeService nationalIdType)
        {
            _nationalIdTypeService = nationalIdType;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetNationalIdTypesPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _nationalIdTypeService.GetNationalIdTypePaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetNationalIdTypes()
        {
            var result = await _nationalIdTypeService.GetNationalIdTypes();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNationalIdType([FromRoute] int id)
        {
            var result = await _nationalIdTypeService.GetNationalIdType(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertNationalIdType([FromBody] NationalIdTypeDto nationalIdTypeDto)
        {
            await _nationalIdTypeService.InsertNationalIdType(nationalIdTypeDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateNationalIdType([FromBody] NationalIdTypeDto nationalIdTypeDto)
        {
            await _nationalIdTypeService.UpdateNationalIdType(nationalIdTypeDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNationalIdType([FromRoute] int id)
        {
            await _nationalIdTypeService.DeleteNationalIdType(id);
            return Ok();
        }
    }
}
