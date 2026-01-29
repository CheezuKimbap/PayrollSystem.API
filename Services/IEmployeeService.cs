using PayrollSystem.API.Dtos;
using PayrollSystem.API.Models;

namespace PayrollSystem.API.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResultDto[]> GetAll();
        Task<EmployeeResultDto?> GetById(Guid id);
        Task<EmployeeResultDto?> GetByEmployeeNumber(string employeeNumber);

        Task<EmployeeSummaryDto> Create(Employee employee);
        Task<EmployeeSummaryDto> Update(Employee employee);
        Task Delete(Guid id);
        Task<PayrollResultDto> Compute(string EmployeeNumber, DateTime startDate, DateTime endDate);


    }
}
