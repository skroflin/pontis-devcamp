using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Administration
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetApplicationsPaged(TableMetadata? tableMetadata = null);
        Task<List<Application>> GetApplications();
        Task<int> GetApplicationsCount();
        Task<Application> GetApplication(int id);
        Task InsertApplication (Application application);
        Task UpdateApplication (Application application);
        Task DeleteApplication (int id);
        Task<Application> GetApplicationByName(string name);
    }
}
