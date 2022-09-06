namespace GlacialBytes.ShortPathService.Persistence.Database.Exceptions
{
  /// <summary>
  /// Ошибка при отсутсвие базы данных.
  /// </summary>
  public class DatabaseNotFoundException : DatabaseException
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    public DatabaseNotFoundException()
      : base(L("ERR_NO_DATABASE"))
    {
    }
  }
}
