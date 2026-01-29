using PayrollSystem.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.API.Dtos
{
    public class CreateEmployeeDto
    {        
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }
                
        public string LastName { get; set; } = null!;
                
        public DateTime DateOfBirth { get; set; }
                
        public decimal DailyRate { get; set; }       
        public string WorkingDays { get; set; }
    }
}
