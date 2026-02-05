using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using TrainingHub.Models;
using TrainingHub.ViewModels;
using TrainingHub.Views;
using System.Collections.Generic;

namespace TrainingHub.Services
{
    public class DialogService : IDialogService
    {
        private Window? GetMainWindow()
        {
            if (
                Application.Current?.ApplicationLifetime
                is IClassicDesktopStyleApplicationLifetime desktop
            )
            {
                return desktop.MainWindow;
            }
            return null;
        }

        public async Task<string?> ShowEmployeeTypeSelectionAsync()
        {
            var mainWindow = GetMainWindow();
            if (mainWindow == null)
                return null;

            var viewModel = new AddEmployeeTypeViewModel();
            var window = new EmployeeTypeWindow { DataContext = viewModel };

            var result = await window.ShowDialog<string?>(mainWindow);
            return result;
        }

        public async Task<Employee?> ShowAddEmployeeDialogAsync(string employeeType, List<Director>? directors = null)
        {
            var mainWindow = GetMainWindow();
            if (mainWindow == null)
                return null;

            var viewModel = new AddEmployeeViewModel(employeeType, directors);
            var window = new AddEmployeeWindow { DataContext = viewModel };

            var result = await window.ShowDialog<Employee?>(mainWindow);
            return result;
        }

        public async Task<bool> ShowEditEmployeeDialogAsync(Employee employee)
        {
            var mainWindow = GetMainWindow();
            if (mainWindow == null)
                return false;

            var viewModel = new EditEmployeeViewModel(employee);
            var window = new EditEmployeeWindow { DataContext = viewModel };

            var result = await window.ShowDialog<bool>(mainWindow);
            return result;
        }

        public async Task<bool> ShowDeleteEmployeeDialogAsync(string title, string message)
        {
            var mainWindow = GetMainWindow();
            if (mainWindow == null)
                return false;

            var window = new DeleteEmployeeWindow(title, message);
            var result = await window.ShowDialog<bool>(mainWindow);
            return result;
        }

        public async Task ShowEmployeeDetailsDialogAsync(Employee employee)
        {
            var mainWindow = GetMainWindow();
            if (mainWindow == null)
                return;

            var viewModel = new EmployeeDetailsViewModel(employee);
            var window = new EmployeeDetailsWindow { DataContext = viewModel };

            await window.ShowDialog(mainWindow);
        }
    }
}
