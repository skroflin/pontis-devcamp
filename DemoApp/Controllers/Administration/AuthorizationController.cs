using DemoApp.Core.Dtos.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.api.Controllers.Administration
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public AuthorizationController(IAuthorizationService authorization)
        {
            _authorizationService = authorization;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetAuthorizationsPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _authorizationService.GetAuthorizationPaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorizations()
        {
            var result = await _authorizationService.GetAuthorizations();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorization([FromRoute] int id)
        {
            var result = await _authorizationService.GetAuthorization(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAuthorization([FromBody] AuthorizationDto authorizationDto)
        {
            await _authorizationService.InsertAuthorization(authorizationDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAuthorization([FromBody] AuthorizationDto authorizationDto)
        {
            await _authorizationService.UpdateAuthorization(authorizationDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorization([FromRoute] int id)
        {
            await _authorizationService.DeleteAuthorization(id);
            return Ok();
        }
    }
}
