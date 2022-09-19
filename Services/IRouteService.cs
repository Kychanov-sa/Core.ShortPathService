using System;

namespace GlacialBytes.Core.ShortPathService.Services
{
  /// <summary>
  /// Интерфейс сервиса маршрутов.
  /// </summary>
  public interface IRouteService
  {
    /// <summary>
    /// Добавляет новый маршрут.
    /// </summary>
    /// <param name="resourceLocation">Размещение ресурса.</param>
    /// <param name="bestBefore">Срок действия маршрута.</param>
    /// <returns>Идентификатор маршрута.</returns>
    string AddRoute(Uri resourceLocation, DateTime? bestBefore);
  }
}
