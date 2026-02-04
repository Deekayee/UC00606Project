using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrainingHub.Models;
using TrainingHub.Services;

namespace TrainingHub.ViewModels;

public partial class EmployeesViewModel : ViewModelBase
{
    private readonly Company _company;
    private readonly IDialogService _dialogService;

    public ObservableCollection<EmployeeViewModel> Employees { get; } = new();

    [ObservableProperty]
    private EmployeeViewModel? _selectedEmployee;

    public EmployeesViewModel(Company company, IDialogService dialogService)
    {
        _company = company;
        _dialogService = dialogService;

        RefreshData();
    }

    public void RefreshData()
    {
        Employees.Clear();
        foreach (var employee in _company.Employees)
        {
            Employees.Add(new EmployeeViewModel(employee));
        }
    }

    [RelayCommand]
    private async Task AddEmployee()
    {
        var type = await _dialogService.ShowEmployeeTypeSelectionAsync();
        if (string.IsNullOrEmpty(type))
            return;

        var newEmployee = await _dialogService.ShowAddEmployeeDialogAsync(type);
        if (newEmployee != null)
        {
            _company.AddEmployee(newEmployee);
            Employees.Add(new EmployeeViewModel(newEmployee));
        }
    }

    [RelayCommand]
    private async Task EditEmployee(EmployeeViewModel employeeViewModel)
    {
        if (employeeViewModel == null)
            return;

        var result = await _dialogService.ShowEditEmployeeDialogAsync(employeeViewModel.Employee);

        if (result)
        {
            // Refresh the list to show updated values
            var index = Employees.IndexOf(employeeViewModel);
            if (index >= 0)
            {
                Employees[index] = new EmployeeViewModel(employeeViewModel.Employee);
            }
        }
    }

    [RelayCommand]
    private async Task RemoveEmployee(EmployeeViewModel employeeViewModel)
    {
        if (employeeViewModel == null)
            return;
        // Show confirmation dialog

        var confirmed = await _dialogService.ShowDeleteEmployeeDialogAsync(
            "Delete Employee",
            $"Are you sure you want to delete {employeeViewModel.FullName} ?\n\nThis action cannot be undone."
        );

        if (confirmed)
        {
            _company.RemoveEmployee(employeeViewModel.Employee);

            Employees.Remove(employeeViewModel);
        }
    }

    [RelayCommand]
    private async Task ShowEmployeeDetails(EmployeeViewModel employeeViewModel)
    {
        if (employeeViewModel == null)
            return;

        await _dialogService.ShowEmployeeDetailsDialogAsync(employeeViewModel.Employee);
    }
}
