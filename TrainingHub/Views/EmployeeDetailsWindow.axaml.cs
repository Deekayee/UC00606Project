using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TrainingHub.Views
{
    public partial class EmployeeDetailsWindow : Window
    {
        public EmployeeDetailsWindow()
        {
            InitializeComponent();
        }

        private void OnClose(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
