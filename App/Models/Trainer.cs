namespace UC00606Project.Models;

public class Trainer
{
    public enum Availability
    {
        Daytime,
        Evening,
        Both,
    }

    public class Trainer : Employee
    {
        private string TeachingSubject { get; set; }
        private Availability Availability { get; set; }
        private decimal HourlyRate { get; set; }  
}