using System;
using System.Collections.ObjectModel;

namespace TrainingHub.Models;

public class Coordinator : Employee
{
    public List<Trainer> Trainers { get; set; }
    public string CoordinationArea { get; set; }

    // Constructors
    // Default constructor
    public Coordinator() { }

    // Parameterized constructor
    public Coordinator(int id, string firstName, string lastName, string address, string phoneNumber,
        DateTime contractStartDate, DateTime contractEndDate, DateTime criminalRecordEndDate, decimal salaryBase,
        string coordinationArea)
        : base(id, firstName, lastName, address, phoneNumber, contractStartDate, contractEndDate, criminalRecordEndDate, salaryBase)
    {
        CoordinationArea = coordinationArea;
    }

    // Methods
    public void AddTrainer(Trainer trainer)
    {
        if (trainer == null) throw new ArgumentNullException(nameof(trainer));
        if (!Trainers.Contains(trainer))
            Trainers.Add(trainer);
    }

    public void RemoveTrainer(Trainer trainer)
    {
        if (trainer == null) throw new ArgumentNullException(nameof(trainer));
        Trainers.Remove(trainer);
    }

    public override decimal CalculateMonthlySalary()
    {
        return SalaryBase;
    }

    // Override ToString
    public override string ToString()
    {
        return $"{base.ToString()}, Role: Coordinator, Area: {CoordinationArea}";
    }
}