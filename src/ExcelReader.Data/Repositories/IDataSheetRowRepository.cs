using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public interface IDataSheetRowRepository : IEntityRepository<DataSheetRowEntity>
{
    Task<IEnumerable<DataSheetRowEntity>> GetByDataSheetIdAsync(int dataSheetId);
}
