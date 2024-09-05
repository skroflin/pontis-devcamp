using DemoApp.Core.Models.Administration;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Administration
{
    public interface AuthorizationRepository
    {
        Task<List<Authorization>> GetAuthorizationsPaged(TableMetadata? metadata = null);
        Task<List<Authorization>> GetAuthorizations();
        Task<int> GetAuthorizationsCount();
        Task<Authorization> GetAuthorization(int id);
        Task InsertAuthorization (Authorization authorization);
        Task UpdateAuthorization (Authorization authorization);
        Task DeleteAuthorization (int id);
    }
}
