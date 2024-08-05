using System.Data.SQLite;
using Dapper;
using ExcelReader.Configurations;
using ExcelReader.Data.Entities;
using Microsoft.Extensions.Options;

namespace ExcelReader.Data.Repositories;

public class DataSheetRepository : SqliteEntityRepository<DataSheetEntity>, IDataSheetRepository
{
    #region Constructors

    public DataSheetRepository(IOptions<ApplicationOptions> options) : base(options)
    {
    }

    #endregion
    #region Methods

    public async Task<IEnumerable<DataSheetEntity>> GetByDataFileIdAsync(int dataFileId)
    {
        var table = GetTableName();

        string query = $"SELECT * FROM {table} WHERE DataFileId = '{dataFileId}';";

        using var connection = new SQLiteConnection(ConnectionString);

        return await connection.QueryAsync<DataSheetEntity>(query);
    }

    #endregion
}
