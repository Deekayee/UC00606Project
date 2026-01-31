using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrainingHub.Models;

namespace TrainingHub.ViewModels;

public partial class EmployeesViewModel : ViewModelBase
{
    private readonly Company _company;
    public ObservableCollection<Employee> Employees { get; } = new();

    // Selected Employee
    [ObservableProperty]
    private Employee? _selectedEmployee;

    // Constructor
    public EmployeesViewModel(Company company)
    {
        _company = company;

        // Initialize the Employees collection with the company's employees
        foreach (var employee in _company.Employees)
            Employees.Add(employee);
    }

    [RelayCommand]
    private void AddEmployee()
    {
        // TODO: open modal to add new employee
    }

    [RelayCommand]
    private void EditEmployee()
    {
        if (SelectedEmployee == null) return;

        // TODO: open modal to edit employee
    }

    [RelayCommand]
    private void RemoveEmployee()
    {
        if (SelectedEmployee == null) return;

        // TODO: confirm and remove employee
    }
}
