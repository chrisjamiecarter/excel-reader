using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class WorkbookRepository : SqliteRepository<WorkbookEntity>, IWorkbookRepository
{
}
