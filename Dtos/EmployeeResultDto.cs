using PayrollSystem.API.Models;

namespace PayrollSystem.API.Dtos
{
    public class EmployeeResultDto
    {
        public Guid Id { get; set; }
        
        public string EmployeeNumber { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public decimal DailyRate { get; set; }
        public WorkingDays WorkingDays { get; set; }
    }
}
