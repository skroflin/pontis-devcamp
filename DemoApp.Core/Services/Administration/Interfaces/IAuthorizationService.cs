using DemoApp.Core.Dtos.Administration;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Administration.Interfaces
{
    public interface IAuthorizationService
    {
        Task<PagedListDto<AuthorizationDto>> GetAuthorizationPaged(TableMetadata? tableMetadata);
        Task<List<AuthorizationDto>> GetAuthorizations();
        Task<int> GetAuthorizationsCount();
        Task<AuthorizationDto> GetAuthorization(int id);
        Task InsertAuthorization(AuthorizationDto authorizationDto);
        Task UpdateAuthorization(AuthorizationDto authorizationDto);
        Task DeleteAuthorization(int id);
    }
}
