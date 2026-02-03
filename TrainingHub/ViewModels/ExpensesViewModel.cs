using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using TrainingHub.Models;
using TrainingHub.Services;

namespace TrainingHub.ViewModels;

public partial class ExpensesViewModel : ViewModelBase
{
    private readonly Company _company;
    private readonly IDateProvider _dateProvider;

    // ===== UI Lists =====
    public ObservableCollection<string> ExpenseMonths { get; } = new();

    // ===== Totals / Filters =====
    [ObservableProperty] private string? selectedExpenseMonth;

    [ObservableProperty] private decimal monthlyEmployeesTotal;
    [ObservableProperty] private decimal monthlyTrainersTotal;
    [ObservableProperty] private decimal monthlyExpenseTotal;

    // ===== Trainer Payment Calculator (ad-hoc) =====
    public ObservableCollection<Trainer> Trainers { get; } = new();

    [ObservableProperty] private DateTimeOffset calcStartDate = DateTimeOffset.Now;
    [ObservableProperty] private DateTimeOffset calcEndDate = DateTimeOffset.Now.AddDays(5);
    [ObservableProperty] private decimal calculatedPayment;
    [ObservableProperty] private Trainer? selectedTrainerForCalc;

    public ExpensesViewModel(Company company, IDateProvider dateProvider)
    {
        _company = company;
        _dateProvider = dateProvider;

        BuildExpenseMonths();
        LoadTrainers();

        // evita ComboBox vazio
        if (ExpenseMonths.Count == 0)
            ExpenseMonths.Add(_dateProvider.Today.ToString("MM/yyyy"));

        var todayMonth = _dateProvider.Today.ToString("MM/yyyy");
        SelectedExpenseMonth = ExpenseMonths.Contains(todayMonth) ? todayMonth : ExpenseMonths.FirstOrDefault();

        Refresh();
    }

    partial void OnSelectedExpenseMonthChanged(string? value) => Refresh();

    private const int MonthsToShow = 6;
    private void BuildExpenseMonths()
    {
        ExpenseMonths.Clear();

        var today = _dateProvider.Today;
        var currentMonthStart = new DateTime(today.Year, today.Month, 1);

        for (int i = 0; i < MonthsToShow; i++)
        {
            var m = currentMonthStart.AddMonths(-i);
            ExpenseMonths.Add(m.ToString("MM/yyyy"));
        }
    }

    private void LoadTrainers()
    {
        Trainers.Clear();
        foreach (var t in _company.Employees.OfType<Trainer>())
            Trainers.Add(t);
    }

    private void Refresh()
    {
        MonthlyEmployeesTotal = 0m;
        MonthlyTrainersTotal = 0m;
        MonthlyExpenseTotal = 0m;

        if (string.IsNullOrWhiteSpace(SelectedExpenseMonth))
            return;

        if (!DateTime.TryParseExact("01/" + SelectedExpenseMonth, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var monthStart))
            return;

        var monthEnd = monthStart.AddMonths(1).AddDays(-1);

        bool OverlapsMonth(Employee e) =>
            e.ContractStartDate.Date <= monthEnd.Date &&
            e.ContractEndDate.Date >= monthStart.Date;

        // 1) Payroll
        MonthlyEmployeesTotal = _company.Employees
            .Where(OverlapsMonth)
            .Sum(e => e.CalculateMonthlySalary());

        // 2) Pagamento a formadores: cursos que iniciam no mês selecionado
        MonthlyTrainersTotal = _company.CalculateTotalTrainerPayments(monthStart.Month, monthStart.Year);

        // Total do mês (Payroll + Trainers)
        MonthlyExpenseTotal = MonthlyEmployeesTotal + MonthlyTrainersTotal;
    }

    [RelayCommand]
    private void CalculatePayment()
    {
        if (SelectedTrainerForCalc is null)
        {
            CalculatedPayment = 0m;
            return;
        }

        int days = (CalcEndDate.Date - CalcStartDate.Date).Days + 1;
        if (days < 0) days = 0;

        CalculatedPayment = days * Course.HoursPerDay * SelectedTrainerForCalc.HourlyRate;
    }
}
