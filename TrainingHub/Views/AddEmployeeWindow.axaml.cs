using Avalonia.Controls;
using Avalonia.Interactivity;
using TrainingHub.ViewModels;

namespace TrainingHub.Views
{
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is AddEmployeeViewModel vm)
            {
                var employee = vm.CreateEmployee();
                Close(employee);
            }
            else
            {
                Close(null);
            }
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            Close(null);
        }
    }
}
