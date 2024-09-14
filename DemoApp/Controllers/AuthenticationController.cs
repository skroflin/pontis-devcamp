using DemoApp.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using DemoApp.Core.Services;

namespace DemoApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthenticationController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            try
            {
                var userDto = await _authService.AuthenticateAsync(
                    loginRequest.Username, loginRequest.Password, loginRequest.ApplicationName);

                return Ok(userDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}
