using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Core.Services.Geolocation;
using DemoApp.Core.Services.Geolocation.Interfaces;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.api.Controllers.Geolocation
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService region)
        {
            _regionService = region;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetRegionsPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _regionService.GetRegionPaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            var result = await _regionService.GetRegions();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegion([FromRoute] int id)
        {
            var result = await _regionService.GetRegion(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPlace([FromBody] RegionDto regionDto)
        {
            await _regionService.InsertRegion(regionDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateDistrict([FromBody] RegionDto regionDto)
        {
            await _regionService.UpdateRegion(regionDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] int id)
        {
            await _regionService.DeleteRegion(id);
            return Ok();
        }
    }
}
