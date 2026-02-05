using System.Threading.Tasks;
using TrainingHub.Models;
using System.Collections.Generic;

namespace TrainingHub.Services
{
    public interface IDialogService
    {
        Task<string?> ShowEmployeeTypeSelectionAsync();
        Task<Employee?> ShowAddEmployeeDialogAsync(string employeeType, List<Director>? directors = null);
        Task<bool> ShowEditEmployeeDialogAsync(Employee employee);
        Task<bool> ShowDeleteEmployeeDialogAsync(string title, string message);
        Task ShowEmployeeDetailsDialogAsync(Employee employee);
    }
}
