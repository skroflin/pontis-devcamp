using DemoApp.Core.Dtos.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Administration
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public AuthorizationService(IAuthorizationRepository authorizationRepository) 
        {
            _authorizationRepository = authorizationRepository;
        }
        public async Task<PagedListDto<AuthorizationDto>> GetAuthorizationPaged(TableMetadata? tableMetadata)
        {
            var count = await _authorizationRepository.GetAuthorizationsCount();
            var authorizations = await _authorizationRepository.GetAuthorizationsPaged(tableMetadata);
            var authorizationDtos = new List<AuthorizationDto>();
            foreach (var authorization in authorizations) 
            {
                authorizationDtos.Add(AuthorizationDto.CreateDto(authorization));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<AuthorizationDto>() { PagedData = authorizationDtos, PagingMetadata = pagingMetadata };
        }

        public async Task<List<AuthorizationDto>> GetAuthorizations()
        {
            var authorizations = await _authorizationRepository.GetAuthorizations();
            var authorizationDtos = new List<AuthorizationDto>();
            foreach(var authorization in authorizations)
            {
                authorizationDtos.Add(AuthorizationDto.CreateDto(authorization));
            }

            return authorizationDtos;
        }
        public async Task<AuthorizationDto> GetAuthorization(int id)
        {
            var authorization = await _authorizationRepository.GetAuthorization(id);
            return AuthorizationDto.CreateDto(authorization);
        }

        public async Task<int> GetAuthorizationsCount()
        {
            return await _authorizationRepository.GetAuthorizationsCount();
        }

        public async Task DeleteAuthorization(int id)
        {
            await _authorizationRepository.DeleteAuthorization(id);
        }

        public async Task InsertAuthorization(AuthorizationDto authorizationDto)
        {
            await _authorizationRepository.InsertAuthorization(AuthorizationDto.ToModel(authorizationDto));
        }

        public async Task UpdateAuthorization(AuthorizationDto authorizationDto)
        {
            await _authorizationRepository.UpdateAuthorization(AuthorizationDto.ToModel(authorizationDto, true));
        }
    }
}
