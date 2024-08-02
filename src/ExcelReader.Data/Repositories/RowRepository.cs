using System.Data.SQLite;
using Dapper;
using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class RowRepository : SqliteRepository<RowEntity>, IRowRepository
{
    public async Task<IEnumerable<RowEntity>> GetByWorksheetIdAsync(int worksheetId)
    {
        string query = $"SELECT * FROM Row WHERE worksheetId = '{worksheetId}';";

        using var connection = new SQLiteConnection(ConnectionString);

        return await connection.QueryAsync<RowEntity>(query);
    }
}
