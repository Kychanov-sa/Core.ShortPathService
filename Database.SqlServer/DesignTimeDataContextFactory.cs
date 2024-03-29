﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GlacialBytes.ShortPathService.Persistence.Database.SqlServer
{
  /// <summary>
  /// Класс фабрики контекстов данных времени разработки.
  /// </summary>
  internal class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<SqlServerDataContext>
  {
    #region IDesignTimeDbContextFactory

    /// <summary>
    /// <see cref="IDesignTimeDbContextFactory{TContext}.CreateDbContext(string[])"/>
    /// </summary>
    public SqlServerDataContext CreateDbContext(string[] args)
    {
      // Контекст использует строку подключения к базе разработки решения.
      var optionsBuilder = new DbContextOptionsBuilder<SqlServerDataContext>();
      optionsBuilder.UseSqlServer("Data Source=;Initial Catalog=;Integrated Security=False;User ID=;Password=;");
      return new SqlServerDataContext(optionsBuilder.Options);
    }
    #endregion
  }
}
