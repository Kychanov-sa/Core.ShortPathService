using System;
using GlacialBytes.Core.ShortPathService.Services.Exceptions;

namespace GlacialBytes.Core.ShortPathService.Services
{
  /// <summary>
  /// Интерфейс сервиса перенаправления.
  /// </summary>
  public interface IRedirectionService
  {
    /// <summary>
    /// Возвращает адрес перенаправления по идентификатору маршртута.
    /// </summary>
    /// <param name="routeId">Идентификатор маршрута.</param>
    /// <returns>Адрес для перенаправления.</returns>
    /// <exception cref="ArgumentException">Идентификатор маршрута имеет некорректное значение.</exception>
    /// <exception cref="RouteNotFoundException">Маршрту не найден.</exception>
    /// <exception cref="RouteExpiredException">Срок действия маршрута истёк.</exception>
    Uri GetRedirectUrl(string routeId);
  }
}
