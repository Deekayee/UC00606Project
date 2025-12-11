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
        public string TeachingSubject { get; set; }
        public Availability Availability { get; set; }
        public decimal HourlyRate { get; set; }  
}