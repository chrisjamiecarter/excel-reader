namespace ExcelReader.Data.Repositories;

public  interface IRepository<TEntity> where TEntity : class
{
    Task<int> AddAsync(TEntity entity);
    Task<int> AddAndGetIdAsync(TEntity entity);
    bool CreateTable();
    Task<int> DeleteAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAsync();
    Task<TEntity?> GetAsync(int id);
    Task<int> UpdateAsync(TEntity entity);
}
