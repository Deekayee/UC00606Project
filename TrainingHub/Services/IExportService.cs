using TrainingHub.Models;

namespace TrainingHub.Services
{
    public interface IExportService
    {
        void ExportEmployeesToCsv(Company company);
    }
}
