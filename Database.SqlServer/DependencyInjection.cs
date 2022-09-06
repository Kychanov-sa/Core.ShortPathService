using GlacialBytes.ShortPathService.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GlacialBytes.ShortPathService.Persistence.Database.SqlServer
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
  }
}
