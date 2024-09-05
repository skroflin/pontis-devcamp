using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Administration
{
    public interface UserRepository : IUserRepository
    {
        Task<List<User>> GetUsersPaged(TableMetadata? metadata = null);
        Task<List<User>> GetUsers();
        Task<int> GetUsersCount();
        Task<User> GetUser(int id);
    }
}
