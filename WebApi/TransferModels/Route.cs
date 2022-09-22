using System;

namespace GlacialBytes.Core.ShortPathService.WebApi.TransferModels
{
  public class Route
  {
    /// <summary>
    /// Идентификатор маршрута.
    /// </summary>
    public string Id { get; set; }

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
