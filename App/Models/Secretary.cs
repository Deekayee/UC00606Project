namespace UC00606Project.Data.Entities;

public class Secretary
{
    private Director ReportsToDirector { get; set; }
    private string Area { get; set; }
}