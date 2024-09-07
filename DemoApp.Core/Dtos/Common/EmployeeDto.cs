using DemoApp.Domain.Models.Common;

namespace DemoApp.Core.Dtos.Common
{
    public record EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string? EmplopyeeName { get; set; }
        public static EmployeeDto CreateDto(Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.Id,
                EmplopyeeName = employee.Username
            };
        }

        public static Employee ToModel(EmployeeDto employeeDto, bool isUpdate = false)
        {
            var employee = new Employee
            {
                Id = employeeDto.EmployeeId,
                Username = employeeDto.EmplopyeeName
            };

            if (isUpdate)
            {
                employee.UserModified = Environment.UserDomainName;
                employee.DateCreated = DateTime.Now;
            }
            else
            {
                employee.UserModified = Environment.UserDomainName;
                employee.DateCreated = DateTime.Now;
            }

            return employee;
        }
    }
}
