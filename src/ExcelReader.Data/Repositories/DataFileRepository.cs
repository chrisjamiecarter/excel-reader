using ExcelReader.Configurations;
using ExcelReader.Data.Entities;
using Microsoft.Extensions.Options;

namespace ExcelReader.Data.Repositories;

public class DataFileRepository : SqliteEntityRepository<DataFileEntity>, IDataFileRepository
{
    #region Constructors

    public DataFileRepository(IOptions<ApplicationOptions> options) : base(options)
    {
    }

    #endregion
}
