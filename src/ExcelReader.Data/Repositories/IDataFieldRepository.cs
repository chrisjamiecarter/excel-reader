using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public interface IDataFieldRepository : IEntityRepository<DataFieldEntity>
{
    Task<IEnumerable<DataFieldEntity>> GetByDataSheetIdAsync(int dataSheetId);
}
