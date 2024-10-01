using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Administration
{
    public interface IRoleAuthorizationRepository
    {
        Task<List<RoleAuthorization>> GetRoleAuthorizationsPaged(TableMetadata? tableMetadata = null);
        Task<List<RoleAuthorization>> GetRoleAuthorizations();
        Task<int> GetRoleAuthorizationsCount();
        Task<RoleAuthorization> GetRoleAuthorization(int roleId, int authorizationId);
        Task InsertRoleAuthorization (RoleAuthorization role);
        Task UpdateRoleAuthorization (RoleAuthorization role);
        Task DeleteRoleAuthorization(int id);
        Task<IEnumerable<RoleAuthorization>> GetRoleAuthorizationForRole(int roleId);
    }
}
