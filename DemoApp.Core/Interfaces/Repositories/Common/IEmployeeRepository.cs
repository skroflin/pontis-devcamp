using DemoApp.Core.Models.Common;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Interfaces.Repositories.Common
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesPaged(TableMetadata? tableMetadata = null);
        Task<List<Employee>> GetEmployees();
        Task<int> GetEmployeesCount();
        Task<Employee> GetEmployee(int id);
        Task InsertEmployee (Employee employee);
        Task UpdateEmployee (Employee employee);
        Task DeleteEmployee (int id);
    }
}
