using Avalonia.Controls;
using Avalonia.Interactivity;
using TrainingHub.ViewModels;

namespace TrainingHub.Views
{
    public partial class EditEmployeeWindow : Window
    {
        public EditEmployeeWindow()
        {
            InitializeComponent();
        }

        private void OnCancel(object? sender, RoutedEventArgs e)
        {
            Close(false);
        }

        private void OnSave(object? sender, RoutedEventArgs e)
        {
            if (DataContext is EditEmployeeViewModel viewModel)
            {
                if (!viewModel.Validate())
                    return;

                viewModel.UpdateEmployee();
                Close(true);
            }
        }
    }
}
