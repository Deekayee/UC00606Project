using System;
using TrainingHub.Services;

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
    public Employee(int id, string firstName, string lastName, string address, string phoneNumber, DateTime contractStartDate, DateTime contractEndDate, DateTime criminalRecordEndDate, decimal salaryBase, IDateProvider dateProvider) : this(dateProvider)
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
    public bool IsContractValid(DateTime referenceDate) => ContractEndDate.Date >= referenceDate.Date && ContractStartDate.Date <= referenceDate.Date;

    // Abstract method to calculate the salary, the implementation is left to the derived classes
    public abstract decimal CalculateMonthlySalary();

    // Method to check if the employee has an expired criminal record
    public bool IsCriminalRecordExpired(DateTime referenceDate) => CriminalRecordEndDate.Date < referenceDate.Date;

    // Override the base ToString method
    public override string ToString()
    {
        return $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, Address: {Address}, PhoneNumber: {PhoneNumber}, ContractStartDate: {ContractStartDate}, ContractEndDate: {ContractEndDate}, CriminalRecordEndDate: {CriminalRecordEndDate}, SalaryBase: {SalaryBase}";
    }
}