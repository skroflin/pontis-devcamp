using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Administration
{
    public class RoleAuthorizationRepository : IRoleAuthorizationRepository
    {
        private readonly AdminDbContext _context;
        public RoleAuthorizationRepository(AdminDbContext context)
        {
            _context = context;
        }
        public async Task DeleteRoleAuthorization(int id)
        {
            var country = await _context.RoleAuthorizations.FindAsync(id);
            if (country != null)
            {
                _context.RoleAuthorizations.Remove(country);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RoleAuthorization> GetRoleAuthorization(int roleId, int authorizationId)
        {
            return await _context.RoleAuthorizations
                .FirstOrDefaultAsync(ra => ra.RoleId == roleId && ra.AuthorizationId == authorizationId);
        }

        public async Task<List<RoleAuthorization>> GetRoleAuthorizationsPaged(TableMetadata? tableMetadata = null)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.RoleAuthorizations
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);
            return await query.ToListAsync();
        }

        public async Task<List<RoleAuthorization>> GetRoleAuthorizations()
        {
            return await _context.RoleAuthorizations.ToListAsync();
        }

        public async Task<int> GetRoleAuthorizationsCount()
        {
            return await _context.RoleAuthorizations.CountAsync();
        }

        public async Task InsertRoleAuthorization(RoleAuthorization Role)
        {
            _context.RoleAuthorizations.Add(Role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAuthorization(RoleAuthorization Role)
        {
            var existingCountry = await _context.RoleAuthorizations.FindAsync(Role.RoleId);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(Role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RoleAuthorization>> GetRoleAuthorizationForRole(int roleId)
        {
            return await _context.RoleAuthorizations
                .Where(ra => ra.RoleId == roleId)
                .ToListAsync();
        }
    }
}
