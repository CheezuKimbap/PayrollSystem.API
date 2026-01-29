using Microsoft.AspNetCore.Mvc;

namespace PayrollSystem.API.Dtos
{
    public class CompuePayrollDto
    {       
        public DateTime StartingDate { get; set; }      
        public DateTime EndingDate {  get; set; }
    }
}
