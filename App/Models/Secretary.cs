namespace UC00606Project.Models;

public class Secretary : Employee
{
    public Director ReportsToDirector { get; set; }
    public string Area { get; set; }
}