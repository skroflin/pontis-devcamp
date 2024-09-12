using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Core.Services.Geolocation.Interfaces;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.api.Controllers.Geolocation
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;

        public DistrictController (IDistrictService district)
        {
            _districtService = district;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetDistrictsPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _districtService.GetDistrictPaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDistricts()
        {
            var result = await _districtService.GetDistricts();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistrict([FromRoute] int id)
        {
            var result = await _districtService.GetDistrict(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDistrict([FromBody] DistrictDto districtDto)
        {
            await _districtService.InsertDistrict(districtDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateDistrict([FromBody] DistrictDto districtDto)
        {
            await _districtService.UpdateDistrict(districtDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict([FromRoute] int id)
        {
            await _districtService.DeleteDistrict(id);
            return Ok();
        }
    }
}
