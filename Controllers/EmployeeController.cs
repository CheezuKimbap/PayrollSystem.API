using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.API.Dtos;
using PayrollSystem.API.Models;
using PayrollSystem.API.Services;
using PayrollSystem.API.Shared;

namespace PayrollSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeResultDto[]>> GetAll()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);

        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResultDto>> GetById(Guid id)
        {
            var employee = await _employeeService.GetById(id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet("by-employee-number/{employeeNumber}")]
        public async Task<ActionResult<EmployeeResultDto>> GetByEmployeeNumber(string employeeNumber)
        {
            var employee = await _employeeService.GetByEmployeeNumber(employeeNumber);

            if (employee == null)
            {
                return NotFound(); 
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeSummaryDto>> Create([FromBody] CreateEmployeeDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.WorkingDays))
                {
                    return BadRequest("WorkingDays is required.");
                }

                if (!Enum.TryParse<WorkingDays>(dto.WorkingDays.Trim(),out var workingDays))
                {
                    return BadRequest(new APIResponse<EmployeeSummaryDto>
                    {
                        Success = false,
                        Message = "WorkingDays must be either 'MWF' or 'TTHS'."
                    });
                }
                var employee = new Employee
                    {
                        FirstName = dto.FirstName,
                        MiddleName = dto.MiddleName,
                        LastName = dto.LastName,
                        DateOfBirth = dto.DateOfBirth,
                        DailyRate = dto.DailyRate,
                        WorkingDays = workingDays
                    };

                var result = await _employeeService.Create(employee);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.Id },
                    new APIResponse<EmployeeSummaryDto>
                    {
                        Success = true,
                        Message = "Employee created successfully",
                        Data = result
                    });

            }          
            catch (InvalidOperationException ex)
            {
                return NotFound(new APIResponse<EmployeeSummaryDto>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<EmployeeSummaryDto>
                {
                    Success = false,
                    Message = ex.Message
                });
            }

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EmployeeSummaryDto>> Update(Guid id, [FromBody]  UpdateEmployeeDto dto)
        {
            try
            {
                var employee = new Employee
                {
                    Id = id,
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    DateOfBirth = dto.DateOfBirth,
                    DailyRate = dto.DailyRate,
                    WorkingDays = dto.WorkingDays,
                };

                var result = await _employeeService.Update(employee);
                return Ok(new APIResponse<EmployeeSummaryDto>
                {
                    Success = true,
                    Message = "Employee updated successfully",
                    Data = result
                });
            }
            catch (InvalidOperationException ex)
            {

                return NotFound(new APIResponse<EmployeeSummaryDto>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {

                await _employeeService.Delete(id);

                return Ok(new APIResponse<EmployeeSummaryDto>
                {
                    Success = true,
                    Message = "Employee deleted successfully",

                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new APIResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{employeeNumber}/compute")]
        public async Task<IActionResult> ComputePayroll([FromRoute] string employeeNumber, [FromQuery] ComputePayrollDto dto) 
        { 
            try
            {                
                var result = await _employeeService.Compute(employeeNumber, dto.StartingDate, dto.EndingDate);

                return Ok(new APIResponse<PayrollResultDto>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new APIResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new APIResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }





    }
}
