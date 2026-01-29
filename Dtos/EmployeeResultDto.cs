using Microsoft.EntityFrameworkCore;
using PayrollSystem.API.Models;

namespace PayrollSystem.API.Dtos
{
    [Keyless]
    public class EmployeeResultDto
    {
        public Guid Id { get; set; }
        public string EmployeeNumber { get; set; } = null!;
    }

}
