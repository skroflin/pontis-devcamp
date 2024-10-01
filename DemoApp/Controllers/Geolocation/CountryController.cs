using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Core.Services.Geolocation.Interfaces;
using DemoApp.Core.Utils.Security.Attributes;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.api.Controllers.Geolocation
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    [Authorize("admin")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService country)
        {
            _countryService = country;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetCountriesPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _countryService.GetCountryPaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _countryService.GetCountries();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry([FromRoute] int id)
        {
            var result = await _countryService.GetCountry(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCountry([FromBody] CountryDto countryDto)
        {
            await _countryService.InsertCountry(countryDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryDto countryDto)
        {
            await _countryService.UpdateCountry(countryDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] int id)
        {
            await _countryService.DeleteCountry(id);
            return Ok();
        }
    }
}
