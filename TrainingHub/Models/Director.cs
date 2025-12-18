using System;

namespace TrainingHub.Models;

public class Director : Employee
{
    // Properties
    public bool FlexibleHours { get; set; }
    public decimal MonthlyBonus { get; set; }
    public bool CompanyCar { get; set; }

    // Constructors
    public Director() { }

    public Director(int id, string firstName, string lastName, string address, string phoneNumber, DateTime contractStartDate, DateTime contractEndDate, DateTime criminalRecordEndDate, decimal salaryBase, bool flexibleHours, decimal monthlyBonus, bool companyCar) : base(id, firstName, lastName, address, phoneNumber, contractStartDate, contractEndDate, criminalRecordEndDate, salaryBase)
    {
        FlexibleHours = flexibleHours;
        MonthlyBonus = monthlyBonus;
        CompanyCar = companyCar;
    }

    // Methods
    public override decimal CalculateMonthlySalary()
    {
        return SalaryBase + MonthlyBonus;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, FlexibleHours: {FlexibleHours}, MonthlyBonus: {MonthlyBonus}, CompanyCar: {CompanyCar}";
    }
}