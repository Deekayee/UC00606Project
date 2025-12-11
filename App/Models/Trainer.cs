namespace UC00606Project.Models;

public class Trainer : Employee
{
    public enum Availability
    {
        Daytime,
        Evening,
        Both,
    }

    public string TeachingSubject { get; set; }
    public Availability TrainerAvailability { get; set; }
    public decimal HourlyRate { get; set; }
}
