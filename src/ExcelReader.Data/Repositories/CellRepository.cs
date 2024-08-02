using System.Data.SQLite;
using Dapper;
using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class CellRepository : SqliteRepository<CellEntity>, ICellRepository
{
    public async Task<IEnumerable<CellEntity>> GetByRowIdAsync(int rowId)
    {
        string query = $"SELECT * FROM Cell WHERE RowId = '{rowId}';";

        using var connection = new SQLiteConnection(ConnectionString);

        return await connection.QueryAsync<CellEntity>(query);
    }
}
