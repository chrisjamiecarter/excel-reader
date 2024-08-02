using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public interface IColumnRepository : IRepository<ColumnEntity>
{
    Task<IEnumerable<ColumnEntity>> GetByWorksheetIdAsync(int worksheetId);
}
