using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TrainingHub.Views;

public partial class ShowSuccessExportWindow : Window
{
    public ShowSuccessExportWindow()
    {
        InitializeComponent();
    }

    private void OnOkClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
