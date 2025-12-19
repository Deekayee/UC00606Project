using System;

namespace TrainingHub.Models;

public class Secretary : Employee
{   
    // Properties
    public Director ReportsToDirector { get; set; }
    public string Area { get; set; }
    
    // Constructor
    // Default constructor
    public Secretary ( ){}
    
    // Paremeterized constructor
    public Secretary(int id, string firstName, string lastName, string address, string phoneNumber,
        DateTime contractStartDate, DateTime contractEndDate, DateTime criminalRecordEndDate, decimal salaryBase,
        Director reportsToDirector, string area) :
        base(id, firstName, lastName, address, phoneNumber, contractStartDate, contractEndDate, criminalRecordEndDate,
            salaryBase)
    {
        ReportsToDirector = reportsToDirector;
        Area = area;
    }
    
    // Methods
    // Method to calculate the montly salary
    public override decimal CalculateMonthlySalary()
    {
        return SalaryBase;
    }

    // Method to represent the secretary as a string
    public override string ToString()
    {
        return $"{FirstName} {LastName} - Secretary ({Area}) - Director: {(ReportsToDirector != null ? $"{ReportsToDirector.FirstName} {ReportsToDirector.LastName}" : "N/A")}";
    }
}