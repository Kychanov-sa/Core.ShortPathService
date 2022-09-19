using System;
using CSharpVitamins;
using GlacialBytes.Core.ShortPathService.Services.Exceptions;
using GlacialBytes.ShortPathService.Domain.Data;
using GlacialBytes.ShortPathService.Domain.Data.DataModels;

namespace GlacialBytes.Core.ShortPathService.Services
{
  /// <summary>
  /// Сервис перенаправления.
  /// </summary>
  internal class RedirectionService : IRedirectionService
  {
    /// <summary>
    /// Провайдер данных.
    /// </summary>
    private readonly IDataProvider _dataProvider;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dataProvider">Провайдер данных.</param>
    public RedirectionService(IDataProvider dataProvider)
    {
      _dataProvider = dataProvider;
    }

    #region IRedirectionService

    /// <summary>
    /// <see cref="IRedirectionService.GetRedirectUrl(string)"/>
    /// </summary>
    public Uri GetRedirectUrl(string routeId)
    {
      if (!ShortGuid.TryParse(routeId, out ShortGuid routeShortId))
        throw new ArgumentException($"Provided routeId value: '{routeId}' is not a valid identifier.");

      var repository = _dataProvider.GetRepository<Route>();
      var route = repository.Get(routeShortId.Guid);
      if (route == null)
        throw new RouteNotFoundException(routeId);

      if (route.BestBefore != null && route.BestBefore.Value < DateTime.UtcNow)
        throw new RouteExpiredException(routeId);

      return new Uri(route.FullUrl);
    }

    #endregion
  }
}