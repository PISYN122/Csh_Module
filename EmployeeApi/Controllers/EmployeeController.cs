using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models;
using EmployeeApi.Services;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public ActionResult<PaginatedResult<Employee>> GetEmployees(
        [FromQuery] string? query,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 2)
    {
        return Ok(_employeeService.GetEmployees(query, page, pageSize));
    }

    [HttpGet("{id}")]
    public ActionResult<Employee> GetEmployeeById(int id)
    {
        var employee = _employeeService.GetEmployeeById(id);
        return employee != null ? Ok(employee) : NotFound(new { message = "Employee not found" });
    }

    [HttpPost]
    public ActionResult<Employee> CreateEmployee([FromBody] Employee newEmployee)
    {
        var createdEmployee = _employeeService.CreateEmployee(newEmployee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
    {
        if (!_employeeService.UpdateEmployee(id, updatedEmployee))
            return NotFound(new { message = "Employee not found" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteEmployee(int id)
    {
        if (!_employeeService.DeleteEmployee(id))
            return NotFound(new { message = "Employee not found" });

        return NoContent();
    }
}
