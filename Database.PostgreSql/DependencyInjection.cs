using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
    public static IServiceCollection AddPostgreSqlDataContext(this IServiceCollection services, string connectionString)
    {
      services.AddDbContext<PostgreSqlDataContext>(options =>
      {
        options.EnableSensitiveDataLogging();
        options.UseNpgsql(connectionString);
      });
      services.AddScoped<DataContext>(provider => provider.GetRequiredService<PostgreSqlDataContext>());
      services.AddGeneralDataProviders();
      return services;
    }
  }
}
