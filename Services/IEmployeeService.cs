using PayrollSystem.API.Dtos;
using PayrollSystem.API.Models;

namespace PayrollSystem.API.Services
{
    public interface IEmployeeService
    {
        Task<Employee[]> GetAll();
        Task<Employee?> GetById(Guid id);
        Task<Employee?> GetByEmployeeNumber(string employeeNumber);

        Task<EmployeeResultDto> Create(Employee employee);
        Task<EmployeeResultDto> Update(Employee employee);
        Task Delete(Guid id);
        Task<PayrollResultDto> Compute(string EmployeeNumber, DateTime startDate, DateTime endDate);


    }
}
