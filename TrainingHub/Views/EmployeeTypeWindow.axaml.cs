using Avalonia.Controls;
using Avalonia.Interactivity;
using TrainingHub.ViewModels;

namespace TrainingHub.Views
{
    public partial class EmployeeTypeWindow : Window
    {
        public EmployeeTypeWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is AddEmployeeTypeViewModel vm)
            {
                Close(vm.SelectedType);
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
