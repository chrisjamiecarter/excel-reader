using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public interface IDataItemRepository : IEntityRepository<DataItemEntity>
{
    Task<IEnumerable<DataItemEntity>> GetByDataSheetRowIdAsync(int dataSheetRowId);
}
