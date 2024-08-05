using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public interface IDataSheetRepository : IEntityRepository<DataSheetEntity>
{
    Task<IEnumerable<DataSheetEntity>> GetByDataFileIdAsync(int dataFileId);
}
