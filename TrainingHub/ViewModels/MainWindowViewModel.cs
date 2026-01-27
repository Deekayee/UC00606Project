using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace TrainingHub.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string message = string.Empty;

    [ObservableProperty]
    private bool showLogin = true;

    [ObservableProperty]
    private bool showDashboard = false;
    [ObservableProperty]
private string headerDateText = DateTime.Now.ToString("dd/MM/yyyy");


    [RelayCommand]
    private void Login()
    {
        Message = string.Empty;

        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            Message = "Username and password are required.";
            return;
        }

        ShowLogin = false;
        ShowDashboard = true;

        Message = $"Welcome, {Username}!";
    }
    [RelayCommand]
    private void Logout()
    {
        
        ShowDashboard = false;
        ShowLogin = true;

        Password = string.Empty;
        Message = "Logged out.";
    }
}