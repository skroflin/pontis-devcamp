using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;
namespace DemoApp.Domain.Interfaces.Repositories.Administration
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersPaged(TableMetadata tableMetadata);
        Task<List<User>> GetUsers();
        Task<int> GetUsersCount();
        Task<User> GetUser(int id);
        Task InsertUser (User user);
        Task UpdateUser (User user);
        Task DeleteUser (int id);

    }
}
