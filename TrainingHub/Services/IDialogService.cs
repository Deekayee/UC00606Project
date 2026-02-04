using System.Threading.Tasks;
using TrainingHub.Models;

namespace TrainingHub.Services
{
    public interface IDialogService
    {
        Task<string?> ShowEmployeeTypeSelectionAsync();
        Task<Employee?> ShowAddEmployeeDialogAsync(string employeeType);
        Task<bool> ShowEditEmployeeDialogAsync(Employee employee);
        Task<bool> ShowDeleteEmployeeDialogAsync(string title, string message);
        Task ShowEmployeeDetailsDialogAsync(Employee employee);
    }
}
