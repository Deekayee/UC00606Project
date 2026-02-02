using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
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

        foreach (var employee in _company.Employees)
            Employees.Add(new EmployeeViewModel(employee));
    }

    [RelayCommand]
    private async Task AddEmployee()
    {
        var type = await _dialogService.ShowEmployeeTypeSelectionAsync();
        if (string.IsNullOrEmpty(type)) return;

        var newEmployee = await _dialogService.ShowAddEmployeeDialogAsync(type);
        if (newEmployee != null)
        {
            _company.AddEmployee(newEmployee);
            Employees.Add(new EmployeeViewModel(newEmployee));
        }
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
