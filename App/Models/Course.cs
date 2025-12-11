namespace UC00606Project.Models;

public class Course
{
    public string CourseName { get; set; }
    public string Area { get; set; }
    public Trainer Trainer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}