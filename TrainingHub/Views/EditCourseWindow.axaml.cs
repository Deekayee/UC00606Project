using Avalonia.Controls;
using Avalonia.Interactivity;
using TrainingHub.ViewModels;

namespace TrainingHub.Views
{
    public partial class EditCourseWindow : Window
    {
        public EditCourseWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is EditCourseViewModel vm)
            {
                if (!vm.Validate())
                {
                    return;
                }
                vm.UpdateCourse();
                Close(true);
            }
            else
            {
                Close(false);
            }
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            Close(false);
        }
    }
}