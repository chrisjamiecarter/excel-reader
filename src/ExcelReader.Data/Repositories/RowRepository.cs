using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class RowRepository : SqliteRepository<RowEntity>, IRowRepository
{
}
