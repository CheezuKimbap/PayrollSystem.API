using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.API.Data;
using PayrollSystem.API.Dtos;
using PayrollSystem.API.Models;
using System.Reflection.Emit;

namespace PayrollSystem.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PayrollDbContext _context;

        public EmployeeRepository(PayrollDbContext context)
        {
            _context = context;
        }
        
        public async Task<Employee[]> GetAll()
        {
            string sql = @"EXEC usp_GetEmployees";
            var result = await _context.Employees.FromSqlRaw(sql)
                .ToListAsync();

            return result.ToArray();
        }

        public async Task<Employee?> GetByEmployeeNumber(string employeeNumber)
        {
            string sql = @"EXEC usp_GetByEmployee @Id, @EmployeeNumber";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", DBNull.Value),
                new SqlParameter("@EmployeeNumber", employeeNumber)
            };

            var employees = await _context.Employees
                .FromSqlRaw(sql, parameters.ToArray())
                .AsNoTracking()
                .ToListAsync();

            return employees.FirstOrDefault();
        }

        public async Task<Employee?> GetById(Guid id)
        {
            string sql = @"EXEC usp_GetByEmployee @Id, @EmployeeNumber";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@EmployeeNumber", DBNull.Value)
            };

            var employees = await _context.Employees
               .FromSqlRaw(sql, parameters.ToArray())
               .AsNoTracking()
               .ToListAsync();

            return employees.FirstOrDefault();
        }

        public async Task<EmployeeSummaryDto> Create(Employee employee)
        {
            string sql = @"EXEC usp_CreateEmployee @FirstName, @MiddleName, @LastName, @DateOfBirth, @DailyRate, @WorkingDays";
            var parameters = new[]
            {
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@MiddleName", (object?)employee.MiddleName ?? DBNull.Value),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@DateOfBirth", employee.DateOfBirth),
                new SqlParameter("@DailyRate", employee.DailyRate),
                new SqlParameter("@WorkingDays", employee.WorkingDays)
            };

            var result =  _context.EmployeeResults
                .FromSqlRaw(sql, parameters)
                .AsNoTracking()
                .AsEnumerable()
                .Single();

            return result;
        }
        public async Task<EmployeeSummaryDto> Update(Employee employee)
        {
            string sql = @"EXEC usp_UpdateEmployee @Id, @FirstName, @MiddleName, @LastName, @DateOfBirth, @DailyRate, @WorkingDays";

            var parameters = new[]
            {
                new SqlParameter("@Id", employee.Id),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@MiddleName", (object?)employee.MiddleName ?? DBNull.Value),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@DateOfBirth", employee.DateOfBirth),
                new SqlParameter("@DailyRate", employee.DailyRate),
                new SqlParameter("@WorkingDays", employee.WorkingDays)
            };

            var result = _context.EmployeeResults
                .FromSqlRaw(sql, parameters.ToArray())
                .AsNoTracking()
                .AsEnumerable()
                .Single();

            return result;


        }
        public async Task Delete(Guid id)
        {
            string sql = @"EXEC usp_DeleteEmployee @Id";

            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            await _context.Database.ExecuteSqlRawAsync(sql,parameters);
        }

        public async Task<bool> ExistsByLastNameAndDateOfBirth(Employee employee)
        {
            return await _context.Employees
            .AnyAsync(e =>
                e.LastName == employee.LastName &&
                e.DateOfBirth == employee.DateOfBirth &&
                e.DeletedAt == null);
        }

        public async Task<decimal> ComputePayroll(Guid id, DateTime startingDate, DateTime endingDate)
        {
            string sql = @"EXEC usp_ComputePayroll @Id, @StartingDate, @EndingDate";
            var parameters = new[]
             {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@StartingDate", startingDate),
                    new SqlParameter("@EndingDate", endingDate),
            };

            var result = _context.Database
                .SqlQueryRaw<decimal>(sql, parameters)
                .AsEnumerable()
                .Single();
            return result;
        }

    }
}
