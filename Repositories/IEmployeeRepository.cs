using PayrollSystem.API.Dtos;
using PayrollSystem.API.Models;

namespace PayrollSystem.API.Repositories
{
   
    public interface IEmployeeRepository
    {
        Task<Employee?> GetById(Guid id);
        Task<Employee?> GetByEmployeeNumber(string employeeNumber);
        Task<Employee[]> GetAll();
        
        
        Task<EmployeeSummaryDto> Create(Employee employee);
        Task<EmployeeSummaryDto> Update(Employee employee);
        Task Delete(Guid id);
        Task<bool> ExistsByLastNameAndDateOfBirth(Employee employee);
        Task<decimal> ComputePayroll(Guid id, DateTime startingDate, DateTime endingDate);

    }
    

}
