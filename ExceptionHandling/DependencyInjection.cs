using GlacialBytes.Foundation.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace GlacialBytes.Core.ShortPathService.Diagnostics
{
  /// <summary>
  /// Инжектор зависимостей.
  /// </summary>
  public static class DependencyInjection
  {
    /// <summary>
    /// Добавляет в зависимости сервисы уровня приложения.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    /// <returns>Дополненная коллекция служб.</returns>
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
      services.AddSingleton<IExceptionHandler, ExceptionHandler>();
      return services;
    }
  }
}
