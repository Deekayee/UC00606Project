using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TrainingHub.Views
{
    public partial class DeleteEmployeeWindow : Window
    {
        // Parameterless constructor for XAML runtime loader
        public DeleteEmployeeWindow()
            : this(string.Empty, string.Empty) { }

        public DeleteEmployeeWindow(string title, string message)
        {
            InitializeComponent();

            var titleTextBlock = this.FindControl<TextBlock>("TitleTextBlock");
            var messageTextBlock = this.FindControl<TextBlock>("MessageTextBlock");

            if (titleTextBlock != null)
            {
                titleTextBlock.Text = title;
            }

            if (messageTextBlock != null)
            {
                messageTextBlock.Text = message;
            }
        }

        private void OnCancel(object? sender, RoutedEventArgs e)
        {
            Close(false);
        }

        private void OnConfirm(object? sender, RoutedEventArgs e)
        {
            Close(true);
        }
    }
}
