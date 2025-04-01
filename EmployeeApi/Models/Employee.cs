namespace EmployeeApi.Models;

public class Employee
{
    public int Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string ComputerInfo { get; set; } = string.Empty;
}
