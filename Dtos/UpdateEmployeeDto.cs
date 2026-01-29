using PayrollSystem.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.API.Dtos
{
    public class UpdateEmployeeDto
    {            
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }    
        public decimal DailyRate { get; set; }
        public WorkingDays WorkingDays { get; set; }
    }
}
