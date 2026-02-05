using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using TrainingHub.Models;

namespace TrainingHub.ViewModels
{
    public partial class AddEmployeeViewModel : ViewModelBase
    {
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
        private DateTimeOffset? _contractStartDate = DateTime.Today;

        [ObservableProperty]
        private DateTimeOffset? _contractEndDate = DateTime.Today.AddYears(1);

        [ObservableProperty]
        private DateTimeOffset? _criminalRecordEndDate = DateTime.Today.AddYears(-1);
        
        [ObservableProperty]
        private decimal _salaryBase = 2000m;

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

        // Error Message
        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        partial void OnErrorMessageChanged(string value)
        {
            OnPropertyChanged(nameof(HasError));
        }

        public bool IsTrainer => EmployeeType == "Trainer";
        public bool IsSecretary => EmployeeType == "Secretary";
        public bool IsCoordinator => EmployeeType == "Coordinator";
        public bool IsDirector => EmployeeType == "Director";

        public AddEmployeeViewModel(string employeeType)
        {
            EmployeeType = employeeType;
        }
        
        public bool Validate()
        {
            ErrorMessage = string.Empty;

            if (
                string.IsNullOrWhiteSpace(FirstName)
                || string.IsNullOrWhiteSpace(LastName)
                || string.IsNullOrWhiteSpace(Address)
                || string.IsNullOrWhiteSpace(PhoneNumber)
                || ContractStartDate is null
                || ContractEndDate is null
                || CriminalRecordEndDate is null
            )
            {
                ErrorMessage = "All fields are required.";
                return false;
            }

            if (ContractEndDate.Value < ContractStartDate.Value)
            {
                ErrorMessage = "Contract start date cannot be later than end date.";
                return false;
            }

            if (CriminalRecordEndDate.Value < ContractStartDate.Value)
            {
                ErrorMessage =
                    "Criminal record end date cannot be earlier than contract start date.";
                return false;
            }

            return true;
        }

        public Employee CreateEmployee()
        {
            // validate here?
            
            Employee emp = EmployeeType switch
            {
                "Director" => new Director(),
                "Coordinator" => new Coordinator(),
                "Secretary" => new Secretary(),
                "Trainer" => new Trainer(),
                _ => throw new InvalidOperationException("Unknown type")
            };
            
            // Common properties
            emp.FirstName = FirstName;
            emp.LastName = LastName;
            emp.Address = Address;
            emp.PhoneNumber = PhoneNumber;
            emp.ContractStartDate = ContractStartDate?.DateTime ?? DateTime.Today;
            emp.ContractEndDate = ContractEndDate?.DateTime ?? DateTime.Today;
            emp.CriminalRecordEndDate = CriminalRecordEndDate?.DateTime ?? DateTime.Today;
            emp.SalaryBase = SalaryBase;

            // Specific properties
            if (emp is Trainer t)
            {
                t.TeachingSubject = Subject;
                t.HourlyRate = Rate;
                t.TrainerAvailability = Trainer.Availability.Both; // Default
            }
            if (emp is Secretary s)
            {
               s.Area = Area;
               // missing to what director this secretary reports to
            }
            if (emp is Coordinator c)
            {
                c.CoordinationArea = CoordinationArea;
            }
            if (emp is Director d)
            {
                d.FlexibleHours = FlexibleHours;
                d.MonthlyBonus = MonthlyBonus;
                d.CompanyCar = CompanyCar;
            }
            
            return emp;
        }
    }
}
