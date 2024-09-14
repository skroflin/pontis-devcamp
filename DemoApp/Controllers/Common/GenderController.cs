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
    [Authorize]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService gender)
        {
            _genderService = gender;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetGendersPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _genderService.GetGenderPaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetGenders()
        {
            var result = await _genderService.GetGenders();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGender([FromRoute] int id)
        {
            var result = await _genderService.GetGender(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertGender([FromBody] GenderDto genderDto)
        {
            await _genderService.InsertGender(genderDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateGender([FromBody] GenderDto genderDto)
        {
            await _genderService.UpdateGender(genderDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender([FromRoute] int id)
        {
            await _genderService.DeleteGender(id);
            return Ok();
        }
    }
}
