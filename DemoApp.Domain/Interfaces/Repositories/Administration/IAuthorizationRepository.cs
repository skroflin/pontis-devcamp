using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Administration
{
    public interface IAuthorizationRepository
    {
        Task<List<Authorization>> GetAuthorizationPaged(TableMetadata? tableMetadata = null);
        Task<List<Authorization>> GetAuthorizations();
        Task<int> GetAuthorizationsCount();
        Task<Authorization> GetAuthorization(int id);
        Task InsertAuthorization(Authorization authorization);
        Task UpdateAuthorization(Authorization authorization);
        Task DeleteAuthorization(int id);
    }
}
