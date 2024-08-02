using ExcelReader.Data.Entities;
using Dapper;
using System.Data.SQLite;

namespace ExcelReader.Data.Repositories;

public class ColumnRepository : SqliteRepository<ColumnEntity>, IColumnRepository
{
    public async Task<IEnumerable<ColumnEntity>> GetByWorksheetIdAsync(int worksheetId)
    {
        string query = $"SELECT * FROM Column WHERE worksheetId = '{worksheetId}';";

        using var connection = new SQLiteConnection(ConnectionString);

        return await connection.QueryAsync<ColumnEntity>(query);
    }
}
