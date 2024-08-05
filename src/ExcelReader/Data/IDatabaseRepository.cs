namespace ExcelReader.Data.Repositories;

public interface IDatabaseRepository
{
    string ConnectionString { get; }

    void EnsureCreated();
    void EnsureDeleted();
}