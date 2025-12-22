using System;

namespace TrainingHub.Models;

public class Course
{
     // Properties
    public string CourseName { get; set; }
    public string Area { get; set; }
    public Trainer Trainer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

// Constants
    public const int HoursPerDay = 6;

    // Constructors
    // Default constructor
    public Course() { }

    // Parameterized constructor
    public Course(string courseName, string area,
        DateTime startDate, DateTime endDate, Trainer trainer)
    {
       
        CourseName = courseName;
        Area = area;
        StartDate = startDate;
        EndDate = endDate;
        Trainer = trainer;
    }

    // Methods
    public int GetDurationInDays()
    {
        if (EndDate.Date < StartDate.Date)
            throw new ArgumentException("EndDate cannot be earlier than StartDate.");

        return (EndDate.Date - StartDate.Date).Days + 1;
    }

    public decimal CalculateTrainerPayment()
    {
        if (Trainer == null)
            throw new InvalidOperationException("Course must have an assigned trainer.");

        return GetDurationInDays() * HoursPerDay * Trainer.HourlyRate;
    }

    // Override ToString
    public override string ToString()
    {
        return $"Course: {CourseName}, Start Date: {StartDate:yyyy-MM-dd}, End Date: {EndDate:yyyy-MM-dd}";
    }
}