using DemoApp.Core.Models.Administration;
using DemoApp.Domain.Paging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
