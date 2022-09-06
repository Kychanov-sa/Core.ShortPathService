using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlacialBytes.ShortPathService.Persistence.Database.PostgreSql
{
  /// <summary>
  /// Класс фабрики контекстов данных времени разработки.
  /// </summary>
  public class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<PostgreSqlDataContext>
  {
    #region IDesignTimeDbContextFactory

    /// <summary>
    /// <see cref="IDesignTimeDbContextFactory{TContext}.CreateDbContext(string[])"/>
    /// </summary>
    public PostgreSqlDataContext CreateDbContext(string[] args)
    {
      // Контекст использует строку подключения к базе разработки решения.
      var optionsBuilder = new DbContextOptionsBuilder<PostgreSqlDataContext>();
      optionsBuilder.UseNpgsql("Host=hrpro-dev-common.rx;Port=5432;Database=SelfServiceOffice_Dev;Username=postgres;Password=11111;");
      return new PostgreSqlDataContext(optionsBuilder.Options);
    }
    #endregion
  }
}
