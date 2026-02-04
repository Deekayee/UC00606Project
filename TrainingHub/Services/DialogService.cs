using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using TrainingHub.Models;
using TrainingHub.ViewModels;
using TrainingHub.Views;

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

        public async Task<Employee?> ShowAddEmployeeDialogAsync(string employeeType)
        {
            var mainWindow = GetMainWindow();
            if (mainWindow == null)
                return null;

            var viewModel = new AddEmployeeViewModel(employeeType);
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

        public async Task<Course?> ShowAddCourseDialogAsync(System.Collections.Generic.IEnumerable<Trainer> trainers)
        {
            var mainWindow = GetMainWindow();
            if (mainWindow == null)
                return null;

            var viewModel = new AddCourseViewModel(trainers);
            var window = new AddCourseWindow { DataContext = viewModel };

            var result = await window.ShowDialog<Course?>(mainWindow);
            return result;
        }

        public async Task<bool> ShowEditCourseDialogAsync(Course course, System.Collections.Generic.IEnumerable<Trainer> trainers)
        {
            var mainWindow = GetMainWindow();
            if (mainWindow == null)
                return false;

            var viewModel = new EditCourseViewModel(course, trainers);
            var window = new EditCourseWindow { DataContext = viewModel };

            var result = await window.ShowDialog<bool>(mainWindow);
            return result;
        }

        public async Task<bool> ShowDeleteCourseDialogAsync(string title, string message)
        {
            var mainWindow = GetMainWindow();
            if (mainWindow == null)
                return false;

            var window = new DeleteCourseWindow(title, message);
            var result = await window.ShowDialog<bool>(mainWindow);
            return result;
        }
    }
}
