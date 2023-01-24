using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GlacialBytes.ShortPathService.Persistence.Database.PostgreSql
{
  /// <summary>
  /// Контекст данных для PostgreSQL.
  /// </summary>
  internal class PostgreSqlDataContext : DataContext
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    public PostgreSqlDataContext()
      : base("Host=;Port=;Database=;Username=;Password=;")
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
  }
}
