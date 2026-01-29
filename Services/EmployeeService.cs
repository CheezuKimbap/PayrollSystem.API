using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.API.Dtos;
using PayrollSystem.API.Models;
using PayrollSystem.API.Repositories;
using PayrollSystem.API.Mapping;

namespace PayrollSystem.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;        
        }

        public async Task<EmployeeResultDto[]> GetAll()
        {
         
            var employees = await _employeeRepository.GetAll();
                        
            return employees.Select(EmployeeMapper.ToResultDto).ToArray();
        }

        public async Task<EmployeeResultDto?> GetById(Guid id)
        {

            var employee = await _employeeRepository.GetById(id);

            if (employee == null)
                return null;

            return EmployeeMapper.ToResultDto(employee);
        }

        public async Task<EmployeeResultDto?> GetByEmployeeNumber(string employeeNumber)
        {

            var employee = await _employeeRepository.GetByEmployeeNumber(employeeNumber);

            if (employee == null)
                return null;

            return EmployeeMapper.ToResultDto(employee);
        }

        public async Task<EmployeeSummaryDto> Create(Employee employee)
        {
           

            if (employee.DailyRate < 0)
            {
                throw new ArgumentException("Daily rate cannot be negative.");
            }

            var exists = await _employeeRepository.ExistsByLastNameAndDateOfBirth(employee);

            if (exists)
            {
                throw new InvalidOperationException("Employee already exists.");
            }

            return await _employeeRepository.Create(employee);
                    
           
        }

        public async Task<EmployeeSummaryDto> Update(Employee employee)
        {
            if (employee.Id == Guid.Empty)
            {
                throw new ArgumentException("Employee Id is required");
            }
                       

            var existing = await _employeeRepository.GetById(employee.Id);

            if (existing == null)
                throw new InvalidOperationException("Employee not found");
            
            return await _employeeRepository.Update(employee);

          
        }

        public async Task Delete(Guid id)
        {
            var existing = await _employeeRepository.GetById(id);

            if (existing == null)
                throw new InvalidOperationException("Employee not found");

            await _employeeRepository.Delete(id);
        }

        public async Task<PayrollResultDto> Compute(string EmployeeNumber, DateTime startingDate, DateTime endingDate)
        {
            if (startingDate > endingDate)
                throw new ArgumentException("Start date cannot be after end date.");

            var employee = await _employeeRepository.GetByEmployeeNumber(EmployeeNumber);
            if (employee == null)
                throw new InvalidOperationException("Employee not found.");

            var totalPay = await _employeeRepository
                .ComputePayroll(employee.Id, startingDate, endingDate);

            return new PayrollResultDto
            {
                EmployeeNumber = employee.EmployeeNumber,
                StartDate = DateOnly.FromDateTime(startingDate),
                EndDate = DateOnly.FromDateTime(endingDate),
                TakeHomePay = totalPay
            };
        }


    }
}
