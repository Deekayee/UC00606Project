using System;
using Avalonia.Media;
using TrainingHub.Models;

namespace TrainingHub.ViewModels;

public class EmployeeViewModel
{
    public Employee Employee { get; }

    public EmployeeViewModel(Employee employee)
    {
        Employee = employee;
    }

    public int Id => Employee.Id;

    public string FullName => $"{Employee.FirstName} {Employee.LastName}";

    public string Position => Employee.GetType().Name;

    public string ContractStart => Employee.ContractStartDate.ToString("d MMM yyyy");

    public string ContractEnd => Employee.ContractEndDate.ToString("d MMM yyyy");

    public Color StatusColor
    {
        get
        {
            var today = DateTime.Today;

            if (!Employee.IsContractValid(today))
                return Colors.Red;

            return Colors.Green;
        }
    }
}