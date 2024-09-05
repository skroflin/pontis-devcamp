using DemoApp.Core.Models.Administration;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Administration
{
    public interface ApplicationRepository
    {
        Task<List<Application>> GetApplicationsPaged(TableMetadata? tableMetadata = null);
        Task<List<Application>> GetApplications();
        Task<int> GetApplicationsCount();
        Task<Application> GetApplication(int id);
        Task InsertApplication(Application application);
        Task UpdateApplication(Application application);
        Task DeleteApplication(int id);
    }
}
