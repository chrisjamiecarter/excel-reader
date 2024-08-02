using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public interface IWorksheetRepository : IRepository<WorksheetEntity>
{
    Task<IEnumerable<WorksheetEntity>> GetByWorkbookIdAsync(int workbookId);
}
