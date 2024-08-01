using ExcelReader.Data.Entities;
using static Dapper.SqlMapper;

namespace ExcelReader.Data.Repositories;

public class DataFileRepository : SqliteRepository<DataFileEntity>, IDataFileRepository
{
    public DataFileRepository()
    {
        CreateTable();
    }
}
