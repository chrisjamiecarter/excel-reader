using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public interface ICellRepository : IRepository<CellEntity>
{
    Task<IEnumerable<CellEntity>> GetByRowIdAsync(int rowId);
}
