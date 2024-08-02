using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public interface IRowRepository : IRepository<RowEntity>
{
    Task<IEnumerable<RowEntity>> GetByWorksheetIdAsync(int worksheetId);
}
