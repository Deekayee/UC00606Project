using System;
using CommunityToolkit.Mvvm.ComponentModel;
using TrainingHub.Models;

namespace TrainingHub.ViewModels
{
    public partial class EditEmployeeViewModel : ViewModelBase
    {
        private readonly Employee _employee;

        public string EmployeeType { get; }

        [ObservableProperty]
        private string _firstName = string.Empty;

        [ObservableProperty]
        private string _lastName = string.Empty;

        [ObservableProperty]
        private string _address = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private DateTimeOffset? _contractStartDate;

        [ObservableProperty]
        private DateTimeOffset? _contractEndDate;

        [ObservableProperty]
        private DateTimeOffset? _criminalRecordEndDate;

        [ObservableProperty]
        private decimal _salaryBase;

        // Trainer specific
        [ObservableProperty]
        private string _subject = string.Empty;

        [ObservableProperty]
        private decimal _rate;

        // Secretary specific
        [ObservableProperty]
        private string _area = string.Empty;

        // Coordinator specific
        [ObservableProperty]
        private string _coordinationArea = string.Empty;

        // Director specific
        [ObservableProperty]
        private bool _flexibleHours;

        [ObservableProperty]
        private decimal _monthlyBonus;

        [ObservableProperty]
        private bool _companyCar;

        public bool IsTrainer => EmployeeType == "Trainer";
        public bool IsNotTrainer => !IsTrainer;
        public bool IsSecretary => EmployeeType == "Secretary";
        public bool IsCoordinator => EmployeeType == "Coordinator";
        public bool IsDirector => EmployeeType == "Director";

        public EditEmployeeViewModel(Employee employee)
        {
            _employee = employee;
            EmployeeType = employee.GetType().Name;

            // Load common properties
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Address = employee.Address;
            PhoneNumber = employee.PhoneNumber;
            ContractStartDate = employee.ContractStartDate;
            ContractEndDate = employee.ContractEndDate;
            CriminalRecordEndDate = employee.CriminalRecordEndDate;
            SalaryBase = employee.SalaryBase;

            // Load specific properties
            if (employee is Trainer trainer)
            {
                Subject = trainer.TeachingSubject;
                Rate = trainer.HourlyRate;
            }
            else if (employee is Secretary secretary)
            {
                Area = secretary.Area;
            }
            else if (employee is Coordinator coordinator)
            {
                CoordinationArea = coordinator.CoordinationArea;
            }
            else if (employee is Director director)
            {
                FlexibleHours = director.FlexibleHours;
                MonthlyBonus = director.MonthlyBonus;
                CompanyCar = director.CompanyCar;
            }
        }

        public void UpdateEmployee()
        {
            _employee.FirstName = FirstName;
            _employee.LastName = LastName;
            _employee.Address = Address;
            _employee.PhoneNumber = PhoneNumber;
            _employee.ContractStartDate = ContractStartDate?.DateTime ?? DateTime.Today;
            _employee.ContractEndDate = ContractEndDate?.DateTime ?? DateTime.Today;
            _employee.CriminalRecordEndDate = CriminalRecordEndDate?.DateTime ?? DateTime.Today;
            _employee.SalaryBase = SalaryBase;

            // Update specific properties
            if (_employee is Trainer trainer)
            {
                trainer.TeachingSubject = Subject;
                trainer.HourlyRate = Rate;
            }
            else if (_employee is Secretary secretary)
            {
                secretary.Area = Area;
            }
            else if (_employee is Coordinator coordinator)
            {
                coordinator.CoordinationArea = CoordinationArea;
            }
            else if (_employee is Director director)
            {
                director.FlexibleHours = FlexibleHours;
                director.MonthlyBonus = MonthlyBonus;
                director.CompanyCar = CompanyCar;
            }
        }
    }
}
