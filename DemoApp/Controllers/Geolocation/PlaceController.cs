using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Core.Services.Geolocation.Interfaces;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.api.Controllers.Geolocation
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService place) 
        {
            _placeService = place;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetPlacesPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _placeService.GetPlacePaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlaces()
        {
            var result = await _placeService.GetPlaces();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlace([FromRoute] int id)
        {
            var result = await _placeService.GetPlace(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPlace([FromBody] PlaceDto placeDto)
        {
            await _placeService.InsertPlace(placeDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateDistrict([FromBody] PlaceDto placeDto)
        {
            await _placeService.UpdatePlace(placeDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace([FromRoute] int id)
        {
            await _placeService.DeletePlace(id);
            return Ok();
        }
    }
}
