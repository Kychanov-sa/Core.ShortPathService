using System;

namespace GlacialBytes.ShortPathService.Domain.Data.Commands
{
  /// <summary>
  /// Аргументы команды удаления маршрутов с истёкшим сроком действия.
  /// </summary>
  public class RemoveExpiredRoutesCommandArgs
  {
    /// <summary>
    /// Срок, после которого маршрут считается устаревшим.
    /// </summary>
    public DateTime ExpiredAfterDate { get; set; }
  }
}
