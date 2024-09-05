using DemoApp.Core.Models.Administration;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Administration
{
    public interface UserRepository
    {
        Task<List<User>> GetUsersPaged(TableMetadata? metadata = null);
        Task<List<User>> GetUsers();
        Task<int> GetUsersCount();
        Task<User> GetUser(int id);
    }
}
