using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using Dapper;
using System.Data.SQLite;
using Database = SQLite;

namespace ExcelReader.Data.Repositories;

public class DapperRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    #region Constants

    private readonly string _databaseName = "ExcelReader";
    
    private readonly string _databaseExtension = ".db";

    #endregion
    #region Constructors

    public DapperRepository()
    {
        EnsureDeleted();
        EnsureCreated();
    }


    #endregion
    #region Properties

    private string ConnectionString => $"Data Source={FileName}";

    private string FileName => Path.ChangeExtension(_databaseName, _databaseExtension);

    private string FilePath => Path.GetFullPath(FileName);

    #endregion
    #region Methods - Public

    public bool CreateTable()
    {
        var database = new Database.SQLiteConnection(FilePath);
        var result = database.CreateTable<TEntity>();

        // HACK:
        // SQLite.CreateTable doesnt care for the Table(DataAnnotations.Schema).
        // So alter the Table name after but leave the generic creation to the library.
        var entityName = typeof(TEntity).Name;
        var tableName = GetTableName();

        if (entityName != tableName)
        {
            var query = $"ALTER TABLE {entityName} RENAME TO {tableName};";
            database.Execute(query);
        }

        return result is Database.CreateTableResult.Created;
    }


    public async Task<int> AddAsync(TEntity entity)
    {
        string tableName = GetTableName();
        string columns = GetColumns(true);
        string properties = GetPropertyNames(true);
        string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

        using var connection = new SQLiteConnection(ConnectionString);
        return await connection.ExecuteAsync(query, entity);
    }

    public async Task<int> DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void EnsureCreated()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();
    }

    public void EnsureDeleted()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    #endregion
    #region Methods - Private
        
    private string GetTableName()
    {
        var type = typeof(TEntity);
        var tableAttribute = type.GetCustomAttribute<TableAttribute>();
        return tableAttribute is null ? type.Name : tableAttribute.Name;
    }

    public static string? GetKeyColumnName()
    {
        PropertyInfo[] properties = typeof(TEntity).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

            if (keyAttributes != null && keyAttributes.Length > 0)
            {
                object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                if (columnAttributes != null && columnAttributes.Length > 0)
                {
                    ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                    return columnAttribute.Name;
                }
                else
                {
                    return property.Name;
                }
            }
        }

        return null;
    }


    private string GetColumns(bool excludeKey = false)
    {
        var type = typeof(TEntity);
        var columns = string.Join(", ", type.GetProperties()
            .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
            .Select(p =>
            {
                var columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                return columnAttr != null ? columnAttr.Name : p.Name;
            }));

        return columns;
    }

    protected string GetPropertyNames(bool excludeKey = false)
    {
        var properties = typeof(TEntity).GetProperties()
            .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

        var values = string.Join(", ", properties.Select(p =>
        {
            return $"@{p.Name}";
        }));

        return values;
    }

    protected string GetPropertyValues()
    {
        var properties = typeof(TEntity).GetProperties();
            //.Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);



        var values = string.Join(", ", properties.Select(p =>
        {
            return $"@{p.Name}";
        }));

        return values;
    }

    protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
    {
        var properties = typeof(TEntity).GetProperties()
            .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

        return properties;
    }

    protected string? GetKeyPropertyName()
    {
        var properties = typeof(TEntity).GetProperties()
            .Where(p => p.GetCustomAttribute<KeyAttribute>() != null);

        if (properties.Any())
        {
            return properties.FirstOrDefault().Name;
        }

        return null;
    }

    #endregion

}
