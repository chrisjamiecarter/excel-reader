using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class CellRepository : SqliteRepository<CellEntity>, ICellRepository
{
}
