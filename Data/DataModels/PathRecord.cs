namespace GlacialBytes.ShortPathService.Domain.Data.DataModels
{
  /// <summary>
  /// Модель данных запись пути.
  /// </summary>
  public class PathRecord : DbObject
  {
    /// <summary>
    /// Полный адрес ресурса.
    /// </summary>
    public string FullUrl { get; set; }
  }
}
