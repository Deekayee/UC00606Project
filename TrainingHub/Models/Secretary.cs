using System;

namespace TrainingHub.Models;

public class Secretary : Employee
{   
    // Properties
<<<<<<< Updated upstream
=======
    // Director this secretary reports to
>>>>>>> Stashed changes
    public Director ReportsToDirector { get; set; }
    // Department/area the secretary belongs to
    public string Area { get; set; }
<<<<<<< Updated upstream
    
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
=======

    // Constructors
    // Default constructor
    public Secretary() { }

    // Parameterized constructor
    public Secretary(
        int id,
        string firstName,
        string lastName,
        string address,
        string phoneNumber,
        DateTime contractStartDate,
        DateTime contractEndDate,
        DateTime criminalRecordEndDate,
        decimal salaryBase,
        Director reportsToDirector,
        string area
    ) : base(id, firstName, lastName, address, phoneNumber, contractStartDate, contractEndDate, criminalRecordEndDate, salaryBase)
    {
        // Validate arguments
        if (reportsToDirector == null) throw new ArgumentNullException(nameof(reportsToDirector));
        if (string.IsNullOrWhiteSpace(area)) throw new ArgumentException("Area cannot be null or whitespace", nameof(area));

        ReportsToDirector = reportsToDirector;
        Area = area;
    }

    // Methods
    // Method to calculate the monthly salary
>>>>>>> Stashed changes
    public override decimal CalculateMonthlySalary()
    {
        return SalaryBase;
    }

    // Method to represent the secretary as a string
    public override string ToString()
    {
<<<<<<< Updated upstream
        return $"{FirstName} {LastName} - Secretary ({Area}) - Director: {(ReportsToDirector != null ? $"{ReportsToDirector.FirstName} {ReportsToDirector.LastName}" : "N/A")}";
=======
        // Include name, role, area, and a null-safe director name
        return $"{base.ToString()},- Secretary ({Area}) - Director: {(ReportsToDirector != null ? $"{ReportsToDirector.FirstName} {ReportsToDirector.LastName}" : "N/A")}";
        
>>>>>>> Stashed changes
    }
}