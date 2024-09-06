using DemoApp.Domain.Interfaces.Repositories.Common;
using DemoApp.Domain.Models.Common;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Common
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CoreDbContext _context;
        public EmployeeRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeesPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Employees
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<int> GetEmployeesCount()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task InsertEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var existingCountry = await _context.Employees.FindAsync(employee.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEmployee(int id)
        {
            var country = await _context.Employees.FindAsync(id);
            if (country != null)
            {
                _context.Employees.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}
