using System;

namespace GlacialBytes.Core.ShortPathService.WebApi.TransferModels
{
  /// <summary>
  /// Модель маршрута.
  /// </summary>
  public class Route
  {
    /// <summary>
    /// Идентификатор маршрута.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Короткий адрес маршрута.
    /// </summary>
    public string ShortUrl { get; set; }

    /// <summary>
    /// Срок действия.
    /// </summary>
    public DateTime? BestBefore { get; set; }
  }
}
