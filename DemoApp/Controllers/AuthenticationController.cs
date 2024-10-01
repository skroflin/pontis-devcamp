using DemoApp.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using DemoApp.Domain.Interfaces.Repositories.Administration;

namespace DemoApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserApplicationRepository _userApplicationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IRoleAuthorizationRepository _roleAuthorizationRepository;

        public AuthenticationController(
            IUserRepository userRepository,
            IApplicationRepository applicationRepository,
            IUserApplicationRepository userApplicationRepository,
            IRoleRepository roleRepository,
            IAuthorizationRepository authorizationRepository,
            IRoleAuthorizationRepository roleAuthorizationRepository
        )
        {
            _userRepository = userRepository;
            _applicationRepository = applicationRepository;
            _userApplicationRepository = userApplicationRepository;
            _roleRepository = roleRepository;
            _authorizationRepository = authorizationRepository;
            _roleAuthorizationRepository = roleAuthorizationRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAndPassword(loginRequest.Username, loginRequest.Password);
                if (user == null) 
                {
                    return Unauthorized("Invalid username or password!");
                }

                var application = await _applicationRepository.GetApplicationByName(loginRequest.ApplicationName);
                if (application == null) 
                {
                    return Unauthorized("Application not found");
                }

                var userApplications = await _userApplicationRepository.GetUserApplications();
                var userApplication = userApplications.FirstOrDefault(ua => ua.UserId == user.Id && ua.ApplicationId == application.Id);

                if (userApplication == null) 
                {
                    return Forbid("User does not have access to this application");
                }

                var role = await _roleRepository.GetRole(userApplication.RoleId);
                if(role == null)
                {
                    return Unauthorized("Role not found");
                }

                var roleAuthorizations = await _roleAuthorizationRepository.GetRoleAuthorizationForRole(role.Id);
                var authorizationNames = new List<string>();

                foreach(var auth in roleAuthorizations)
                {
                    var authorization = await _authorizationRepository.GetAuthorization(auth.AuthorizationId);
                    if (authorization != null)
                    {
                        authorizationNames.Add(authorization.Name);
                    }
                }

                var response = new AuthResponseDto
                {
                    Username = user.Username,
                    UserRole = role.Name,
                    RoleAuthorizations = authorizationNames
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
