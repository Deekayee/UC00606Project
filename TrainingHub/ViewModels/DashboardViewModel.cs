using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrainingHub.Models;
using TrainingHub.Services;

namespace TrainingHub.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly Company _company;
    private readonly IDateProvider _dateProvider;

    [ObservableProperty] private int _totalEmployees;
    [ObservableProperty] private int _activeContracts;
    [ObservableProperty] private int _expiredCriminalRecords;
    [ObservableProperty] private decimal _totalMonthlyExpenses;
    [ObservableProperty] private int _activeCourses;
    
    public ObservableCollection<string> Notifications { get; } = new();

    public DashboardViewModel(Company company, IDateProvider dateProvider)
    {
        _company = company;
        _dateProvider = dateProvider;
        
        _dateProvider.DateChanged += UpdateStats;
        
        UpdateStats();
    }

    private void UpdateStats()
    {
        DateTime today = _dateProvider.Today;

        TotalEmployees = _company.Employees.Count;
        ActiveContracts = _company.GetValidContracts().Count;
        
        ExpiredCriminalRecords = _company.GetExpiredCriminalRecords().Count;
        TotalMonthlyExpenses = _company.CalculateTotalMonthlyExpense();
        
        // Active Courses: Start <= Today <= End
        ActiveCourses = _company.Courses.Count(c => c.StartDate.Date <= today && c.EndDate.Date >= today);

        UpdateNotifications(today);
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
        _dateProvider.AdvanceDays(30);
    }

    [RelayCommand]
    private void ResetDate()
    {
        _dateProvider.ResetDate();
    }
}
