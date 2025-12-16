using System;
using System.Reflection;

namespace TrainingHub.Models;

public class Trainer : Employee
{
    // Enum representing the available time periods.
    public enum Availability
    {
        Daytime,
        Evening,
        Both,
    }
    // Properties
        public string TeachingSubject { get; set; }
        public Availability TrainerAvailability { get; set; }
        public decimal HourlyRate { get; set; }  
        
        // Constructors
        // Default constructor
        public Trainer(){}
        
        // Paremeterized constructor
        public Trainer(int id, string firstName, string lastName, string address, string phoneNumber,
            DateTime contractStartDate, DateTime contractEndDate, DateTime criminalRecordEndDate, decimal salaryBase,
            string teachingSubject, Availability trainerAvailability, decimal hourlyRate) :
            base(id, firstName, lastName, address, phoneNumber, contractStartDate, contractEndDate, criminalRecordEndDate,
                salaryBase)
        {
          TeachingSubject = teachingSubject;
          TrainerAvailability = trainerAvailability;
          HourlyRate = hourlyRate;
        }
        
        // Methods
        // Method to calculate the monthly salary for the trainer
        public override decimal CalculateMonthlySalary()
        {
            var courses =
                Company.Trainings.Where(c => c.Trainer == this && c.StartDate.Month == Company.CurrentDate.Month);
            decimal total = 0;

            foreach (var c in courses)
            
                total += (decimal)c.CalculateTrainingPayment();
                
            return total;    
            
        }
        
        // Method to represent the trainer as a string
        public override string ToString()
        {
            return $"{FirstName} {LastName} - Trainer ({TeachingSubject})  - Availability: {TrainerAvailability} - Hourly Rate: {HourlyRate:C}";
        }
}