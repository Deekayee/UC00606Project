using System;
using TrainingHub.Models;

namespace TrainingHub.ViewModels
{
    public class EmployeeDetailsViewModel : ViewModelBase
    {
        private readonly Employee _employee;

        public string EmployeeType { get; }
        public int Id => _employee.Id;
        public string FirstName => _employee.FirstName;
        public string LastName => _employee.LastName;
        public string FullName => $"{_employee.FirstName} {_employee.LastName}";
        public string Address => _employee.Address;
        public string PhoneNumber => _employee.PhoneNumber;
        public string ContractStartDate => _employee.ContractStartDate.ToString("dd/MM/yyyy");
        public string ContractEndDate => _employee.ContractEndDate.ToString("dd/MM/yyyy");
        public string CriminalRecordEndDate =>
            _employee.CriminalRecordEndDate.ToString("dd/MM/yyyy");
        public string SalaryBase => _employee.SalaryBase.ToString("C2");
        public string ContractStatus =>
            _employee.IsContractValid(DateTime.Today) ? "Active" : "Expired";

        // Trainer specific
        public string Subject { get; }
        public string Rate { get; }
        public string Availability { get; }

        // Secretary specific
        public string Area { get; }

        // Coordinator specific
        public string CoordinationArea { get; }

        // Director specific
        public string FlexibleHours { get; }
        public string MonthlyBonus { get; }
        public string CompanyCar { get; }

        public bool IsTrainer => EmployeeType == "Trainer";
        public bool IsSecretary => EmployeeType == "Secretary";
        public bool IsCoordinator => EmployeeType == "Coordinator";
        public bool IsDirector => EmployeeType == "Director";

        public EmployeeDetailsViewModel(Employee employee)
        {
            _employee = employee;
            EmployeeType = employee.GetType().Name;

            // Load specific properties
            if (employee is Trainer trainer)
            {
                Subject = trainer.TeachingSubject;
                Rate = trainer.HourlyRate.ToString("C2");
                Availability = trainer.TrainerAvailability.ToString();
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
                FlexibleHours = director.FlexibleHours ? "Yes" : "No";
                MonthlyBonus = director.MonthlyBonus.ToString("C2");
                CompanyCar = director.CompanyCar ? "Yes" : "No";
            }
        }
    }
}
