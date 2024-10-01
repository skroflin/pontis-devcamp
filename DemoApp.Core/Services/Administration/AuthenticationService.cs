using DemoApp.Domain.Models.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Administration;

namespace DemoApp.Core.Services.Administration
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            return await _userRepository.GetUserByUsernameAndPassword(username, password);
        }
    }
}
