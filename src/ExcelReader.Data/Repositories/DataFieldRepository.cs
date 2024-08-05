using ExcelReader.Data.Entities;
using Dapper;
using System.Data.SQLite;
using ExcelReader.Configurations;
using Microsoft.Extensions.Options;

namespace ExcelReader.Data.Repositories;

public class DataFieldRepository : SqliteEntityRepository<DataFieldEntity>, IDataFieldRepository
{
    #region Constructors

    public DataFieldRepository(IOptions<ApplicationOptions> options) : base(options)
    {
    }

    #endregion
    #region Methods

    public async Task<IEnumerable<DataFieldEntity>> GetByDataSheetIdAsync(int dataSheetId)
    {
        var table = GetTableName();

        string query = $"SELECT * FROM {table} WHERE DataSheetId = '{dataSheetId}';";

        using var connection = new SQLiteConnection(ConnectionString);

        return await connection.QueryAsync<DataFieldEntity>(query);
    }

    #endregion
}
