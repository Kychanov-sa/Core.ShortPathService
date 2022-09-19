using System;

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
    Uri GetRedirectUrl(string routeId);
  }
}
