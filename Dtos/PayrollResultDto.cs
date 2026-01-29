namespace PayrollSystem.API.Dtos
{
    public class PayrollResultDto
    {
        public string EmployeeNumber { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TakeHomePay { get; set; }
    }
}
