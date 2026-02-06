using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TrainingHub.Views;

public partial class ShowErrorExportWindow : Window
{
    public ShowErrorExportWindow(string message)
    {
        InitializeComponent();
        ErrorMessageText.Text = message;
    }

    private void OnOkClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
