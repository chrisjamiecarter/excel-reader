using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class WorksheetRepository : SqliteRepository<WorksheetEntity>, IWorksheetRepository
{
}
