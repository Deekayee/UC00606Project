namespace UC00606Project.Models;

public class Course
{
    private string CourseName { get; set; }
    private string Area { get; set; }
    private Trainer Trainer { get; set; }
    private DateTime StartDate { get; set; }
    private DateTime EndDate { get; set; }
}