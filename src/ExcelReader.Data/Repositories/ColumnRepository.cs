using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class ColumnRepository : SqliteRepository<ColumnEntity>, IColumnRepository
{
}
