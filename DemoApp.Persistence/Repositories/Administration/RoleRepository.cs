using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Administration
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AdminDbContext _context;
        public RoleRepository(AdminDbContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetRolesPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Roles
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<int> GetRolesCount()
        {
            return await _context.Roles.CountAsync();
        }

        public async Task<Role> GetRole(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task InsertRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRole(Role role)
        {
            var existingCountry = await _context.Roles.FindAsync(role.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRole(int id)
        {
            var country = await _context.Roles.FindAsync(id);
            if (country != null)
            {
                _context.Roles.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}
