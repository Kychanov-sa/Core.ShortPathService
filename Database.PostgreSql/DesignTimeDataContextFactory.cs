using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GlacialBytes.ShortPathService.Persistence.Database.PostgreSql
{
  /// <summary>
  /// Класс фабрики контекстов данных времени разработки.
  /// </summary>
  internal class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<PostgreSqlDataContext>
  {
    #region IDesignTimeDbContextFactory

    /// <summary>
    /// <see cref="IDesignTimeDbContextFactory{TContext}.CreateDbContext(string[])"/>
    /// </summary>
    public PostgreSqlDataContext CreateDbContext(string[] args)
    {
      // Контекст использует строку подключения к базе разработки решения.
      var optionsBuilder = new DbContextOptionsBuilder<PostgreSqlDataContext>();
      optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgre;Username=postgre;Password=;");
      return new PostgreSqlDataContext(optionsBuilder.Options);
    }
    #endregion
  }
}
