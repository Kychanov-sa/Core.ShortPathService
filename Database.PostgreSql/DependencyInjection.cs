using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Directum.SelfServiceOffice.Domain.Data;
using Directum.SelfServiceOffice.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GlacialBytes.ShortPathService.Persistence.Database.PostgreSql
{
  /// <summary>
  /// Инжектор зависимостей.
  /// </summary>
  public static class DependencyInjection
  {
    /// <summary>
    /// Добавляет в зависимости провайдеры данных.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <returns>Дополненная коллекция служб.</returns>
    public static IServiceCollection AddSqlServerDataProviders(this IServiceCollection services, string connectionString)
    {
      services.AddDbContext<SqlServerDataContext>(options =>
      {
        options.EnableSensitiveDataLogging();
        options.UseSqlServer(connectionString);
      });
      services.AddScoped<DataContext>(provider => provider.GetRequiredService<SqlServerDataContext>());
      services.AddScoped<IDataProvider, DataProvider>();
      return services;
    }

    /// <summary>
    /// Добавляет проверки схемы данных.
    /// </summary>
    /// <param name="builder">Построитель проверок здоровья.</param>
    /// <param name="failureStatus">Статус нарушения проверки.</param>
    /// <param name="tags">Теги проверок.</param>
    /// <returns>Дополненный построитель проверок здоровья.</returns>
    public static IHealthChecksBuilder AddSqlServerDatabaseSchemeChecks(
      this IHealthChecksBuilder builder,
      HealthStatus? failureStatus,
      IEnumerable<string> tags)
    {
      builder.AddCheck<SqlServerDatabaseSchemeCheck>("Database scheme check", failureStatus, tags);
      return builder;
    }
  }
}
