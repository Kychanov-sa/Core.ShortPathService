using System;

namespace GlacialBytes.ShortPathService.Domain.Data.Commands
{
  /// <summary>
  /// Аргументы команды добавления нового маршрута.
  /// </summary>
  public class AddRouteCommandArgs
  {
    /// <summary>
    /// Адрес полнолго пути к ресурсу.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Срок действия маршрута.
    /// </summary>
    public DateTime? BestBefore { get; set; }
  }
}
