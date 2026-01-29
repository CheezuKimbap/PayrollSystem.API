using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.API.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required]
        public string EmployeeNumber { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;        
        public string? MiddleName { get; set; }              
        [Required]
        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public decimal DailyRate { get; set; } = 0;
        public WorkingDays WorkingDays { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? ModifiedAt {  get; set; }

    }
}
