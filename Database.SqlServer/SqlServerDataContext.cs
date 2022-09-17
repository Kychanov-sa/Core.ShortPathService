using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GlacialBytes.ShortPathService.Persistence.Database.SqlServer
{
  /// <summary>
  /// Контекст данных.
  /// </summary>
  internal class SqlServerDataContext : DataContext
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    public SqlServerDataContext()
      : base("Data Source=ORPD5SQL01\\SQL2017;Initial Catalog=EssSimpleSign_Dev;Integrated Security=False;User ID=sa;Password=Orpd5Admin!;")
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="options">Опции контекста.</param>
    /// <param name="forceMigration">Признак необходимости проведения миграции.</param>
    public SqlServerDataContext(DbContextOptions<SqlServerDataContext> options, bool forceMigration = false)
      : base(options, forceMigration)
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <param name="forceMigration">Признак необходимости проведения миграции.</param>
    /// <param name="create">Признак необходимости создания базы, если её нет.</param>
    public SqlServerDataContext(string connectionString, bool forceMigration = false, bool create = true)
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
        optionsBuilder.UseSqlServer(_connectionString);
      }
      optionsBuilder.UseLazyLoadingProxies();
    }

    /// <summary>
    /// <see cref="DataContext.GetDataBaseSchemeVersion"/>
    /// </summary>
    public override string GetDataBaseSchemeVersion()
    {
      var parameterReturn = new Microsoft.Data.SqlClient.SqlParameter
      {
        ParameterName = "@ReturnValue",
        SqlDbType = System.Data.SqlDbType.VarChar,
        Direction = System.Data.ParameterDirection.Output,
        Size = 50,
      };
      _ = Database.ExecuteSqlRaw("[dbo].[GetDataSchemeVersion] @ReturnValue OUT", parameterReturn);
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
