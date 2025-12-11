using System;

namespace TrainingHub.Models;

public abstract class Employee
{
    // Properties
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime ContractStartDate { get; set; }
    public DateTime ContractEndDate { get; set; }
    public DateTime CriminalRecordEndDate { get; set; }
    public decimal SalaryBase { get; set; }

    // Constructors

    // Default constructor
    public Employee() { }

    // Parameterized constructor
    public Employee(int id, string firstName, string lastName, string address, string phoneNumber, DateTime contractStartDate, DateTime contractEndDate, DateTime criminalRecordEndDate, decimal salaryBase)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumber = phoneNumber;
        ContractStartDate = contractStartDate;
        ContractEndDate = contractEndDate;
        CriminalRecordEndDate = criminalRecordEndDate;
        SalaryBase = salaryBase;
    }

    // Methods

    // Method to check if the contract is valid
    public bool IsContractValid => ContractEndDate < DateTime.Now;

    // Abstract method to calculate the salary, the implementation is left to the derived classes
    public abstract decimal CalculateMonthlySalary();

    // Method to check if the employee has an expired criminal record
    public bool HasExpiredCriminalRecord => CriminalRecordEndDate < DateTime.Now;

    // Abstract to string method
    public abstract override string ToString();
}