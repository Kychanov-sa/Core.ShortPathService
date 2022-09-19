namespace GlacialBytes.Core.ShortPathService.Services.Exceptions
{
  /// <summary>
  /// Срок действия маршрута истёк.
  /// </summary>
  public class RouteExpiredException : BaseApplicationException
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="routeId">Идентификатор запрошенного маршрута.</param>
    public RouteExpiredException(string routeId)
      : base(L("ERR_ROUTE_EXPIRED", routeId))
    {
    }
  }
}
