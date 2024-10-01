using DemoApp.Domain.Models.Administration;

namespace DemoApp.Core.Services.Administration.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<User> Authenticate(string username, string password);
    }
}
