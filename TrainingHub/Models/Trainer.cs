using System;
<<<<<<< Updated upstream
using System.Reflection;
=======
using System.Collections.Generic;
using System.Linq;
>>>>>>> Stashed changes

namespace TrainingHub.Models;

public class Trainer : Employee
{
<<<<<<< Updated upstream
=======
    // Availability indicates when the trainer can teach
>>>>>>> Stashed changes
    // Enum representing the available time periods.
    public enum Availability
    {
        // Available during daytime hours
        Daytime,
        // Available during evening hours
        Evening,
        // Available during both daytime and evening
        Both,
    }
<<<<<<< Updated upstream
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
=======
        // Properties
        public string TeachingSubject { get; set; }
        public Availability TrainerAvailability { get; set; }
        public decimal HourlyRate { get; set; }
        public List<Course> AssignedCourses { get; } = new List<Course>();

        public Trainer() { }

        // Parameterized constructor
        public Trainer(
            int id,
            string firstName,
            string lastName,
            string address,
            string phoneNumber,
            DateTime contractStartDate,
            DateTime contractEndDate,
            DateTime criminalRecordEndDate,
            decimal salaryBase,
            string teachingSubject,
            Availability trainerAvailability,
            decimal hourlyRate
        ) : base(id, firstName, lastName, address, phoneNumber, contractStartDate, contractEndDate, criminalRecordEndDate, salaryBase)
        {
            TeachingSubject = teachingSubject;
            TrainerAvailability = trainerAvailability;
            HourlyRate = hourlyRate;
        }

        // Methods
        // Method to calculate total trainer payments for courses starting in the current month
        public override decimal CalculateMonthlySalary()
        {
            var nowDate = DateTime.Now;
            var coursesThisMonth = AssignedCourses.Where(c => c.Trainer == this && c.StartDate.Month == nowDate.Month && c.StartDate.Year == nowDate.Year);
            decimal total = 0m;
            foreach (var c in coursesThisMonth)
            {
                total += c.CalculateTrainerPayment();
            }
            return total;
        }
        // Method to represent the trainer as a string
        public override string ToString()
        {
            return $"{base.ToString()} - Trainer ({TeachingSubject}) - Availability: {TrainerAvailability} - Hourly Rate: {HourlyRate:C}";
        }

}
>>>>>>> Stashed changes
