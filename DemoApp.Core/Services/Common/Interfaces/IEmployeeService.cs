using DemoApp.Core.Dtos.Administration;
using DemoApp.Core.Dtos.Common;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Common.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedListDto<EmployeeDto>> GetEmployeePaged(TableMetadata? tableMetadata);
        Task<List<EmployeeDto>> GetEmployees();
        Task<int> GetUsersCount();
        Task<EmployeeDto> GetEmployee(int id);
        Task InsertEmployee (EmployeeDto employeeDto);
        Task UpdateEmployee (EmployeeDto employeeDto);
        Task DeleteEmployee(int id);    
    }
}
