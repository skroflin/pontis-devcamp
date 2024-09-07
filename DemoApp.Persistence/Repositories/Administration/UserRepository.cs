using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Administration
{
    public class UserRepository : IUserRepository
    {
        private readonly AdminDbContext _context;
        public UserRepository(AdminDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUsersPaged(TableMetadata tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Users
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }
        public async Task DeleteUser(int id)
        {
            var country = await _context.Employees.FindAsync(id);
            if (country != null)
            {
                _context.Employees.Remove(country);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<int> GetUsersCount()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task InsertUser(User user)
        {
            _context.Employees.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            var existingCountry = await _context.Employees.FindAsync(user.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
