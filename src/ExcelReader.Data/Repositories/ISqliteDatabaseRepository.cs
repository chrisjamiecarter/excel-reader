namespace ExcelReader.Data.Repositories;

public interface ISqliteDatabaseRepository
{
    void EnsureCreated();
    void EnsureDeleted();
}