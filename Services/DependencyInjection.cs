using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GlacialBytes.Foundation.Data;
using GlacialBytes.ShortPathService.Domain.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GlacialBytes.Core.ShortPathService.Services
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
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddScoped<IRouteService, RouteService>();
      services.AddScoped<IRedirectionService, RedirectionService>();
      return services;
    }
  }
}
