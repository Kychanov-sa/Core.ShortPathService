using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GlacialBytes.ShortPathService.Persistence.Database.PostgreSql
{
  /// <summary>
  /// Контекст данных для PostgreSQL.
  /// </summary>
  public class PostgreSqlDataContext : DataContext
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    public PostgreSqlDataContext()
      : base("Host=hrpro-dev-common.rx;Port=5432;Database=SelfServiceOffice_Dev;Username=postgres;Password=11111;")
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="options">Опции контекста.</param>
    public PostgreSqlDataContext(DbContextOptions<PostgreSqlDataContext> options)
      : base(options)
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <param name="forceMigration">Признак необходимости проведения миграции.</param>
    /// <param name="create">Признак необходимости создания базы, если её нет.</param>
    public PostgreSqlDataContext(string connectionString, bool forceMigration = false, bool create = true)
      : base(connectionString, forceMigration, create)
    {
    }

    /// <summary>
    /// <see cref="DbContext.OnConfiguring(DbContextOptionsBuilder)"/>
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseNpgsql(_connectionString);
      }
      optionsBuilder.UseLazyLoadingProxies();
    }

    /// <summary>
    /// <see cref="DataContext.GetDataBaseSchemeVersion"/>
    /// </summary>
    public override string GetDataBaseSchemeVersion()
    {
      var parameterReturn = new Npgsql.NpgsqlParameter
      {
        ParameterName = "@ReturnValue",
        NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar,
        Direction = System.Data.ParameterDirection.Output,
        Size = 50,
      };
      _ = Database.ExecuteSqlRaw("CALL essgetdataschemeversion(null);", parameterReturn);
      return parameterReturn.Value.ToString();
    }

    /// <summary>
    /// <see cref="DataContext.GetDataContextSchemeVersion"/>
    /// </summary>
    public override string GetDataContextSchemeVersion()
    {
      return $"1.0.{Database.GetMigrations().Count()}";
    }
  }
}
