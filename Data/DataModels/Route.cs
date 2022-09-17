using System;

namespace GlacialBytes.ShortPathService.Domain.Data.DataModels
{
  /// <summary>
  /// Модель данных запись пути.
  /// </summary>
  public class Route : DbObject
  {
    /// <summary>
    /// Полный адрес ресурса.
    /// </summary>
    public string FullUrl { get; set; }

    /// <summary>
    /// Срок действия записи.
    /// </summary>
    public DateTime? BestBefore { get; set; }
  }
}
