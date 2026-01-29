using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.API.Dtos;
using PayrollSystem.API.Models;
using PayrollSystem.API.Repositories;

namespace PayrollSystem.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;        
        }

        public async Task<Employee[]> GetAll()
        {
            return await _employeeRepository.GetAll();
        }

        public async Task<Employee?> GetById(Guid id)
        {
            return await _employeeRepository.GetById(id);
        }

        public async Task<Employee?> GetByEmployeeNumber(string employeeNumber)
        {
            return await _employeeRepository.GetByEmployeeNumber(employeeNumber);
        }

        public async Task<EmployeeResultDto> Create(Employee employee)
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

            var result = await _employeeRepository.Create(employee);

            return new EmployeeResultDto
            {
                Id = result.Id,
                EmployeeNumber = result.EmployeeNumber
            };           
           
        }

        public async Task<EmployeeResultDto> Update(Employee employee)
        {
            if (employee.Id == Guid.Empty)
            {
                throw new ArgumentException("Employee Id is required");
            }

            

            var existing = await _employeeRepository.GetById(employee.Id);

            if (existing == null)
                throw new InvalidOperationException("Employee not found");
            
            var result = await _employeeRepository.Update(employee);

            return new EmployeeResultDto()
            {
                Id = result.Id,
                EmployeeNumber = result.EmployeeNumber
            };
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
