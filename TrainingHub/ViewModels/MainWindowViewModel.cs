using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrainingHub.Models;
using TrainingHub.Services;

namespace TrainingHub.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IDateProvider _dateProvider;
    private readonly Company _company;
    private readonly IExportService _exportService;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string message = string.Empty;

    [ObservableProperty]
    private bool isError;

    [ObservableProperty]
    private bool isLoggedIn;

    public bool ShowLogin => !IsLoggedIn;
    public bool ShowDashboard => IsLoggedIn;
    public string HeaderDateText => _dateProvider.Today.ToString("dd/MM/yyyy");

    [ObservableProperty]
    private string currentPage = "Dashboard";

    public bool IsDashboardVisible => CurrentPage == "Dashboard";
    public bool IsEmployeesVisible => CurrentPage == "Employees";
    public bool IsCoursesVisible => CurrentPage == "Courses";
    public bool IsExpensesVisible => CurrentPage == "Expenses";

    public ExpensesViewModel Expenses { get; }
    public EmployeesViewModel Employees { get; }
    public CoursesViewModel Courses { get; }
    public DashboardViewModel Dashboard { get; }

    public MainWindowViewModel(IDialogService dialogService)
    {
        _dateProvider = new DateProvider();
        _company = new Company(_dateProvider);
        _exportService = new ExportService(_dateProvider);

        _dateProvider.DateChanged += () => OnPropertyChanged(nameof(HeaderDateText));

        DemoSeeder.Seed(_company);

        Dashboard = new DashboardViewModel(_company, _dateProvider);
        Expenses = new ExpensesViewModel(_company, _dateProvider);
        Employees = new EmployeesViewModel(_company, dialogService, _exportService);
        Courses = new CoursesViewModel(_company, dialogService);
    }

    partial void OnIsLoggedInChanged(bool value)
    {
        OnPropertyChanged(nameof(ShowLogin));
        OnPropertyChanged(nameof(ShowDashboard));
    }

    partial void OnCurrentPageChanged(string value)
    {
        OnPropertyChanged(nameof(IsDashboardVisible));
        OnPropertyChanged(nameof(IsEmployeesVisible));
        OnPropertyChanged(nameof(IsCoursesVisible));
        OnPropertyChanged(nameof(IsExpensesVisible));

        // Refresh data when switching to a new page
        switch (value)
        {
            case "Courses":
                Courses.RefreshData();
                break;
            case "Expenses":
                Expenses.RefreshData();
                break;
            case "Dashboard":
                Dashboard.RefreshData();
                break;
            case "Employees":
                Employees.RefreshData();
                break;
        }
    }

    [RelayCommand]
    private void GoDashboard() => CurrentPage = "Dashboard";

    [RelayCommand]
    private void GoEmployees() => CurrentPage = "Employees";

    [RelayCommand]
    private void GoCourses() => CurrentPage = "Courses";

    [RelayCommand]
    private void GoExpenses() => CurrentPage = "Expenses";

    [RelayCommand]
    private void Login()
    {
        Message = string.Empty;

        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            Message = "Please fill in all fields.";
            IsError = true;
            return;
        }

        if (Username == "admin" && Password == "1234")
        {
            IsError = false;
            IsLoggedIn = true;
            CurrentPage = "Dashboard";
            return;
        }

        Message = "Incorrect username or password.";
        IsError = true;
    }

    [RelayCommand]
    private void Logout()
    {
        IsLoggedIn = false;

        Username = string.Empty;
        Password = string.Empty;
        Message = string.Empty;
        IsError = false;

        CurrentPage = "Dashboard";
    }
}
