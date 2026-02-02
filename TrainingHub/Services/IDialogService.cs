using System.Threading.Tasks;
using TrainingHub.Models;

namespace TrainingHub.Services
{
    public interface IDialogService
    {
        Task<string?> ShowEmployeeTypeSelectionAsync();
        Task<Employee?> ShowAddEmployeeDialogAsync(string employeeType);
    }
}
