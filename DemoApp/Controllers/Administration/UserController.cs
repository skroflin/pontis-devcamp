using DemoApp.Core.Dtos.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.api.Controllers.Administration
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService user)
        {
            _userService = user;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetUsersPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _userService.GetUserPaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GeUser([FromRoute] int id)
        {
            var result = await _userService.GetUser(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] UserDto userDto)
        {
            await _userService.InsertUser(userDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            await _userService.UpdateUser(userDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }
    }
}
