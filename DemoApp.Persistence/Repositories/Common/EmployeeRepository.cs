using DemoApp.Core.Models.Common;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Common
{
    public interface EmployeeRepository
    {
        Task<List<Employee>> GetEmployeesPaged(TableMetadata? tableMetadata = null);
        Task<List<Employee>> GetEmployees();
        Task<int> GetEmployeesCount();
        Task<Employee> GetEmployee(int employeeId);
        Task InsertEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
    }
}
