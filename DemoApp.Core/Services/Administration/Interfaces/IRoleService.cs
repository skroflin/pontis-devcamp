using DemoApp.Core.Dtos.Administration;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Administration.Interfaces
{
    public interface IRoleService
    {
        Task<PagedListDto<RoleDto>> GetRolePaged(TableMetadata? tableMetadata);
        Task<List<RoleDto>> GetRoles();
        Task<int> GetRolesCount();
        Task<RoleDto> GetRole(int id);
        Task InsertRole (RoleDto roleDto);
        Task UpdateRole (RoleDto roleDto);
        Task DeleteRole (int id);
    }
}
