using System;
using System.IO;
using System.Linq;
using TrainingHub.Models;

namespace TrainingHub.Services
{
    public class ExportService : IExportService
    {
        private readonly IDateProvider _dateProvider;

        public ExportService(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
        }

        public void ExportEmployeesToCsv(Company company)
        {
            string exportFolder = "Exports";
            if (!Directory.Exists(exportFolder))
            {
                Directory.CreateDirectory(exportFolder);
            }

            string fileName = $"Employees-{_dateProvider.Today:dd-MM-yyyy}.csv";
            string fullPath = Path.Combine(exportFolder, fileName);

            using (StreamWriter wr = new StreamWriter(fullPath))
            {
                wr.WriteLine(
                    "Id;Type;FirstName;LastName;Address;PhoneNumber;ContractStartDate;ContractEndDate;CriminalRecordEndDate;MonthlySalary"
                );

                foreach (Employee employee in company.Employees)
                {
                    decimal monthlySalary = CalculateMonthlySalary(employee, company);

                    string line =
                        $"{employee.Id};"
                        + $"{employee.GetType().Name};"
                        + $"{employee.FirstName};"
                        + $"{employee.LastName};"
                        + $"{employee.Address};"
                        + $"{employee.PhoneNumber};"
                        + $"{employee.ContractStartDate:yyyy-MM-dd};"
                        + $"{employee.ContractEndDate:yyyy-MM-dd};"
                        + $"{employee.CriminalRecordEndDate:yyyy-MM-dd};"
                        + $"{monthlySalary}";

                    wr.WriteLine(line);
                }
            }

            Console.WriteLine($"FilePath: {Path.GetFullPath(fullPath)}");
        }

        private decimal CalculateMonthlySalary(Employee employee, Company company)
        {
            if (employee is Trainer trainer)
            {
                var today = _dateProvider.Today;

                var coursesThisMonth = company
                    .Courses.Where(c =>
                        c.Trainer != null
                        && c.Trainer.Id == trainer.Id
                        && c.StartDate.Month == today.Month
                        && c.StartDate.Year == today.Year
                    )
                    .ToList();

                decimal totalPayment = 0;
                foreach (var course in coursesThisMonth)
                {
                    decimal payment = course.CalculateTrainerPayment();
                    totalPayment += payment;
                }
                return totalPayment;
            }

            return employee.CalculateMonthlySalary();
        }
    }
}
