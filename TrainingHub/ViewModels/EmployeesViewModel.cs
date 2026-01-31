using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrainingHub.Models;

namespace TrainingHub.ViewModels;

public partial class EmployeesViewModel : ViewModelBase
{
    private readonly Company _company;

    public ObservableCollection<EmployeeViewModel> Employees { get; } = new();

    [ObservableProperty]
    private EmployeeViewModel? _selectedEmployee;

    public EmployeesViewModel(Company company)
    {
        _company = company;

        foreach (var employee in _company.Employees)
            Employees.Add(new EmployeeViewModel(employee));
    }

    [RelayCommand]
    private void AddEmployee()
    {
        // TODO: open modal to add new employee
    }

    [RelayCommand]
    private void EditEmployee(EmployeeViewModel employee)
    {
        if (employee == null)
            return;

        SelectedEmployee = employee;

        // TODO: open modal to edit employee
    }

    [RelayCommand]
    private void RemoveEmployee(EmployeeViewModel employee)
    {
        if (employee == null)
            return;

        // TODO: confirm and remove employee
    }
}
