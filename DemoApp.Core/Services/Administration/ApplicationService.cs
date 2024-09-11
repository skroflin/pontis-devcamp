using DemoApp.Core.Dtos.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Service.Administration
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<PagedListDto<ApplicationDto>> GetApplicationPaged(TableMetadata? tableMetadata)
        {
            var count = await _applicationRepository.GetApplicationsCount();
            var applications = await _applicationRepository.GetApplicationsPaged(tableMetadata);
            var applicationDtos = new List<ApplicationDto>();
            foreach (var application in applications)
            {
                applicationDtos.Add(ApplicationDto.CreateDto(application));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<ApplicationDto>() { PagedData = applicationDtos, PagingMetadata = pagingMetadata };
        }

        public async Task<List<ApplicationDto>> GetApplications()
        {
            var applications = await _applicationRepository.GetApplications();
            var applicationDtos = new List<ApplicationDto>();
            foreach (var application in applications)
            {
                applicationDtos.Add(ApplicationDto.CreateDto(application));
            }

            return applicationDtos;
        }

        public async Task<int> GetApplicationsCount()
        {
            return await _applicationRepository.GetApplicationsCount();
        }

        public async Task<ApplicationDto> GetApplication(int id)
        {
            var application = await _applicationRepository.GetApplication(id);
            return ApplicationDto.CreateDto(application);
        }

        public async Task InsertApplication(ApplicationDto applicationDto)
        {
            await _applicationRepository.InsertApplication(ApplicationDto.ToModel(applicationDto));
        }

        public async Task UpdateApplication(ApplicationDto applicationDto)
        {
            await _applicationRepository.UpdateApplication(ApplicationDto.ToModel(applicationDto, true));
        }

        public async Task DeleteApplication(int id)
        {
            await _applicationRepository.DeleteApplication(id);
        }

    }
}
