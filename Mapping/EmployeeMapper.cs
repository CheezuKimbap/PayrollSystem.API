using PayrollSystem.API.Dtos;
using PayrollSystem.API.Models;

namespace PayrollSystem.API.Mapping
{
    public static class EmployeeMapper
    {
        public static EmployeeResultDto ToResultDto(Employee employee)
        {
            return new EmployeeResultDto
            {
                Id = employee.Id,
                EmployeeNumber = employee.EmployeeNumber,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                DailyRate = employee.DailyRate,
                WorkingDays = employee.WorkingDays
            };
        }
        public static EmployeeSummaryDto ToSummaryDto(Employee employee)
        {
            return new EmployeeSummaryDto
            {
                Id = employee.Id,
                EmployeeNumber = employee.EmployeeNumber
            };
        }

    }

}
