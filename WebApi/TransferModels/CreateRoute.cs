using System;

namespace GlacialBytes.Core.ShortPathService.WebApi.TransferModels
{
  /// <summary>
  /// Модель создания нового маршрута.
  /// </summary>
  public class CreateRoute
  {
    /// <summary>
    /// Адрес маршрута.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Срок действия.
    /// </summary>
    public DateTime? BestBefore { get; set; }
  }
}
