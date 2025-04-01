using EmployeeApi.Models;

namespace EmployeeApi.Services;

public class EmployeeService
{
    private readonly List<Employee> _employees = new()
    {
        new Employee { Id = 1, LastName = "Іванов", RoomNumber = "101", Department = "IT", ComputerInfo = "Dell XPS 15" },
        new Employee { Id = 2, LastName = "Петров", RoomNumber = "102", Department = "HR", ComputerInfo = "HP EliteBook" }
    };

    public PaginatedResult<Employee> GetEmployees(string? query, int page, int pageSize)
    {
        var filtered = _employees
            .Where(e => string.IsNullOrEmpty(query) || e.LastName.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();

        var totalRecords = filtered.Count;

        return new PaginatedResult<Employee>
        {
            Data = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalRecords = totalRecords
        };
    }

    public Employee? GetEmployeeById(int id) => _employees.FirstOrDefault(e => e.Id == id);

    public Employee CreateEmployee(Employee newEmployee)
    {
        newEmployee.Id = _employees.Max(e => e.Id) + 1;
        _employees.Add(newEmployee);
        return newEmployee;
    }

    public bool UpdateEmployee(int id, Employee updatedEmployee)
    {
        var employee = GetEmployeeById(id);
        if (employee == null) return false;

        employee.LastName = updatedEmployee.LastName;
        employee.RoomNumber = updatedEmployee.RoomNumber;
        employee.Department = updatedEmployee.Department;
        employee.ComputerInfo = updatedEmployee.ComputerInfo;
        return true;
    }

    public bool DeleteEmployee(int id)
    {
        var employee = GetEmployeeById(id);
        if (employee == null) return false;

        _employees.Remove(employee);
        return true;
    }
}
