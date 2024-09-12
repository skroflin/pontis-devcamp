using DemoApp.Core.Dtos.Common;
using DemoApp.Core.Services.Common.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Common;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Common
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployee(id);
            return EmployeeDto.CreateDto(employee);
        }

        public async Task<PagedListDto<EmployeeDto>> GetEmployeePaged(TableMetadata? tableMetadata)
        {
            var count = await _employeeRepository.GetEmployeesCount();
            var employees = await _employeeRepository.GetEmployeesPaged(tableMetadata);
            var employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees) 
            {
                employeeDtos.Add(EmployeeDto.CreateDto(employee));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<EmployeeDto> { PagedData = employeeDtos, PagingMetadata = pagingMetadata };
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployees();
            var employeeDtos = new List<EmployeeDto>();
            foreach(var employee in employees)
            {
                employeeDtos.Add(EmployeeDto.CreateDto(employee));
            }
            return employeeDtos;

        }

        public async Task<int> GetUsersCount()
        {
            return await _employeeRepository.GetEmployeesCount();
        }

        public async Task InsertEmployee(EmployeeDto employeeDto)
        {
            await _employeeRepository.InsertEmployee(EmployeeDto.ToModel(employeeDto));
        }

        public async Task UpdateEmployee(EmployeeDto employeeDto)
        {
            await _employeeRepository.UpdateEmployee(EmployeeDto.ToModel(employeeDto, true));
        }
    }
}
