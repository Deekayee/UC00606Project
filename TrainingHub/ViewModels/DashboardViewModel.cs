using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrainingHub.Models;
using TrainingHub.Services;

namespace TrainingHub.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    // Dependencies
    private readonly Company _company;
    private readonly IDateProvider _dateProvider;

    // Main stats for the cards
    [ObservableProperty] private int _totalEmployees;
    [ObservableProperty] private int _activeContracts;
    [ObservableProperty] private int _expiredCriminalRecords;
    [ObservableProperty] private decimal _totalMonthlyExpenses;
    [ObservableProperty] private int _activeCourses;
    [ObservableProperty] private int _totalCourses;
    
    // Subtexts for the cards
    [ObservableProperty] private string _totalEmployeesSubtext = string.Empty;
    [ObservableProperty] private string _activeContractsSubtext = string.Empty;
    [ObservableProperty] private string _expiredCriminalRecordsSubtext = string.Empty;
    [ObservableProperty] private string _activeCoursesSubtext = string.Empty;
    [ObservableProperty] private string _expensesSubtext = string.Empty;
    
    private decimal _previousMonthExpenses;
    
    public ObservableCollection<string> Notifications { get; } = new();


    public DashboardViewModel(Company company, IDateProvider dateProvider)
    {
        _company = company;
        _dateProvider = dateProvider;
        
        _dateProvider.DateChanged += UpdateStats;
        
        UpdateStats();
    }

    // Update stats based on current date
    private void UpdateStats()
    {
        DateTime today = _dateProvider.Today;

        TotalEmployees = _company.Employees.Count;
        ActiveContracts = _company.GetValidContracts().Count;
        
        ExpiredCriminalRecords = _company.GetExpiredCriminalRecords().Count;
        TotalMonthlyExpenses = _company.CalculateTotalMonthlyExpense();
        
        // Active Courses: Start <= Today <= End
        ActiveCourses = _company.Courses.Count(c => c.StartDate.Date <= today && c.EndDate.Date >= today);
        TotalCourses = _company.Courses.Count;
        
        UpdateSubtexts(today);
        UpdateNotifications(today);
    }
    
    private void UpdateSubtexts(DateTime today)
    {
        // Total Employees Subtext - Show breakdown by type
        var trainers = _company.Employees.OfType<Trainer>().Count();
        var directors = _company.Employees.OfType<Director>().Count();
        var coordinators = _company.Employees.OfType<Coordinator>().Count();
        var secretaries = _company.Employees.OfType<Secretary>().Count();
        
        // Formatting the subtext to show breakdown by type
        var typeParts = new List<string>();
        if (trainers > 0) typeParts.Add($"{trainers} Trainer{(trainers > 1 ? "s" : "")}");
        if (directors > 0) typeParts.Add($"{directors} Director{(directors > 1 ? "s" : "")}");
        if (coordinators > 0) typeParts.Add($"{coordinators} Coordinator{(coordinators > 1 ? "s" : "")}");
        if (secretaries > 0) typeParts.Add($"{secretaries} Secretar{(secretaries > 1 ? "ies" : "y")}");
        
        TotalEmployeesSubtext = typeParts.Count > 0 ? string.Join(", ", typeParts) : "No employees";
        
        // Active Contracts Subtext
        if (TotalEmployees > 0)
        {
            double contractsPercentage = (double)ActiveContracts / TotalEmployees * 100;
            ActiveContractsSubtext = $"{contractsPercentage:F0}% of total employees";
        }
        else
        {
            ActiveContractsSubtext = "No employees";
        }
        
        // Expired Criminal Records Subtext
        if (TotalEmployees > 0)
        {
            double expiredPercentage = (double)ExpiredCriminalRecords / TotalEmployees * 100;
            ExpiredCriminalRecordsSubtext = $"{expiredPercentage:F0}% of total employees";
        }
        else
        {
            ExpiredCriminalRecordsSubtext = "No employees";
        }
        
        // Active Courses Subtext
        if (TotalCourses > 0)
        {
            double coursesPercentage = (double)ActiveCourses / TotalCourses * 100;
            ActiveCoursesSubtext = $"{coursesPercentage:F0}% of total courses";
        }
        else
        {
            ActiveCoursesSubtext = "No courses";
        }
        
        // Expenses Subtext - compare with previous month
        if (_previousMonthExpenses > 0)
        {
            decimal difference = TotalMonthlyExpenses - _previousMonthExpenses;
            double percentageChange = (double)(difference / _previousMonthExpenses * 100);
            
            if (percentageChange > 0)
            {
                ExpensesSubtext = $"{Math.Abs(percentageChange):F0}% more than last month";
            }
            else if (percentageChange < 0)
            {
                ExpensesSubtext = $"{Math.Abs(percentageChange):F0}% less than last month";
            }
            else
            {
                ExpensesSubtext = "Same as last month";
            }
        }
        else
        {
            ExpensesSubtext = "First month data";
            _previousMonthExpenses = TotalMonthlyExpenses;
        }
    }

    private void UpdateNotifications(DateTime today)
    {
        Notifications.Clear();

        // Expired Contracts
        var expiredContracts = _company.Employees
            .Where(e => e.ContractEndDate.Date < today)
            .ToList();

        foreach (var emp in expiredContracts)
        {
            Notifications.Add($"EXPIRED CONTRACT: {emp.FirstName} {emp.LastName} (Ended {emp.ContractEndDate:d})");
        }

        // About to expire (next 30 days)
        var expiringContracts = _company.Employees
            .Where(e => e.ContractEndDate.Date >= today && e.ContractEndDate.Date <= today.AddDays(30))
            .ToList();

        foreach (var emp in expiringContracts)
        {
            Notifications.Add($"Contract expiring soon: {emp.FirstName} {emp.LastName} (Ends {emp.ContractEndDate:d})");
        }

        // Expired Criminal Records
        var expiredRecords = _company.GetExpiredCriminalRecords();
        foreach (var emp in expiredRecords)
        {
            Notifications.Add($"EXPIRED CRIMINAL RECORD: {emp.FirstName} {emp.LastName} (Ended {emp.CriminalRecordEndDate:d})");
        }

        // Criminal Records expiring soon (next 30 days)
        var expiringRecords = _company.Employees
            .Where(e => !e.IsCriminalRecordExpired(today) && e.CriminalRecordEndDate.Date <= today.AddDays(30))
            .ToList();

        foreach (var emp in expiringRecords)
        {
            Notifications.Add($"Criminal Record expiring soon: {emp.FirstName} {emp.LastName} (Ends {emp.CriminalRecordEndDate:d})");
        }
    }

    [RelayCommand]
    private void AdvanceDay()
    {
        _dateProvider.AdvanceDays(1);
    }

    [RelayCommand]
    private void AdvanceMonth()
    {
        _previousMonthExpenses = TotalMonthlyExpenses;
        _dateProvider.AdvanceDays(30);
    }

    [RelayCommand]
    private void ResetDate()
    {
        _dateProvider.ResetDate();
    }
}
