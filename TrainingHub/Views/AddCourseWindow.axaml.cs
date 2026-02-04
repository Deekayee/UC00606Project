using Avalonia.Controls;
using Avalonia.Interactivity;
using TrainingHub.ViewModels;

namespace TrainingHub.Views
{
    public partial class AddCourseWindow : Window
    {
        public AddCourseWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is AddCourseViewModel vm)
            {
                if (!vm.Validate())
                {
                    return;
                }
                var course = vm.CreateCourse();
                Close(course);
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