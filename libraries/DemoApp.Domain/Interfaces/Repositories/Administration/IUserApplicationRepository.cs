using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Administration
{
    public interface IUserApplicationRepository
    {
        Task<List<UserApplication>> GetUserApplicationsPaged(TableMetadata? tableMetadata = null);
        Task<List<UserApplication>> GetUserApplications();
        Task<int> GetUserApplicationsCount();
        Task<UserApplication> GetUserApplication(int userId, int applicationId, int roleId);
        Task InsertUserApplication (UserApplication user);
        Task UpdateUserApplication (UserApplication user);
        Task DeleteUserApplication (int userId, int applicationId, int roleId);
    }
}
