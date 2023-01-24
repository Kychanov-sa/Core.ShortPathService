using GlacialBytes.Foundation.Diagnostics;
using GlacialBytes.ShortPathService.Persistence.Database.Exceptions;
using Microsoft.EntityFrameworkCore;
using DataModels = GlacialBytes.ShortPathService.Domain.Data.DataModels;

namespace GlacialBytes.ShortPathService.Persistence.Database
{
  /// <summary>
  /// Контекст данных.
  /// </summary>
  public abstract class DataContext : DbContext
  {
    /// <summary>
    /// Строка подключения к базе данных.
    /// </summary>
    protected readonly string _connectionString;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public DataContext()
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    public DataContext(string connectionString)
    {
      _connectionString = connectionString;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="options">Опции контекста.</param>
    /// <param name="forceMigration">Признак необходимости проведения миграции.</param>
    public DataContext(DbContextOptions<DataContext> options, bool forceMigration = false)
      : base(options)
    {
      if (forceMigration)
      {
        Database.Migrate();
      }

      InitializeDatabase();
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <param name="forceMigration">Признак необходимости проведения миграции.</param>
    /// <param name="create">Признак необходимости создания базы, если её нет.</param>
    public DataContext(string connectionString, bool forceMigration = false, bool create = true)
    {
      DebugAssert.IsNotNullOrEmpty(connectionString);

      _connectionString = connectionString;
      if (create && !forceMigration)
        Database.EnsureCreated();
      if (forceMigration)
        Database.Migrate();
      if (!Database.CanConnect())
        throw new DatabaseNotFoundException();

      InitializeDatabase();
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="options">Опции контекста.</param>
    /// <param name="forceMigration">Признак необходимости проведения миграции.</param>
    /// <remark>Хак для преобразования опций контекста к разным потомкам.</remark>
    protected DataContext(DbContextOptions options, bool forceMigration = false)
        : base(options)
    {
      if (forceMigration)
      {
        Database.Migrate();
      }

      InitializeDatabase();
    }

    /// <summary>
    /// Инициализация базы данных.
    /// </summary>
    private void InitializeDatabase()
    {
      Log.Debug($"Connection to database {Database.GetDbConnection().Database} at {Database.GetDbConnection().DataSource} established through {Database.ProviderName}.");
    }

    #region DbContext

    /// <summary>
    /// <see cref="DbContext.OnModelCreating(ModelBuilder)"/>
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Записи путей
      modelBuilder.Entity<DataModels.Route>().ToTable("Routes");
      modelBuilder.Entity<DataModels.Route>().HasKey(r => r.Id);
      modelBuilder.Entity<DataModels.Route>().Property(r => r.Id).ValueGeneratedNever();
      modelBuilder.Entity<DataModels.Route>().Property(r => r.FullUrl).IsRequired();
    }
    #endregion
  }
}
