using DemoApp.Core.Dtos.Administration;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Administration.Interfaces
{
    public interface IApplicationService
    {
        Task<PagedListDto<ApplicationDto>> GetApplicationPaged(TableMetadata? tableMetadata);
        Task<List<ApplicationDto>> GetApplications();
        Task<int> GetApplicationsCount();
        Task<ApplicationDto> GetApplication(int id);
        Task InsertApplication(ApplicationDto applicationDto);
        Task UpdateApplication(ApplicationDto applicationDto);
        Task DeleteApplication(int id);
    }
}
