namespace GlacialBytes.Core.ShortPathService.Services.Exceptions
{
  /// <summary>
  /// Маршрут не найден.
  /// </summary>
  public class RouteNotFoundException : BaseApplicationException
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="routeId">Идентификатор запрошенного маршрута.</param>
    public RouteNotFoundException(string routeId)
      : base(L("ERR_ROUTE_NOT_FOUND", routeId))
    {
      ErrorCode = "ERR_ROUTE_NOT_FOUND";
    }
  }
}
