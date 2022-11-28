using System;
using Ardalis.GuardClauses;
using CSharpVitamins;
using GlacialBytes.ShortPathService.Domain.Data;
using GlacialBytes.ShortPathService.Domain.Data.Commands;

namespace GlacialBytes.Core.ShortPathService.Services
{
  /// <summary>
  /// Сервис маршрутов.
  /// </summary>
  internal class RouteService : IRouteService
  {
    /// <summary>
    /// Провайдер данных.
    /// </summary>
    private readonly IDataProvider _dataProvider;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dataProvider">Провайдер данных.</param>
    public RouteService(IDataProvider dataProvider)
    {
      _dataProvider = dataProvider;
    }

    #region IRouteService

    /// <summary>
    /// <see cref="IRouteService.AddRoute(Uri, DateTime?)"/>
    /// </summary>
    public string AddRoute(Uri resourceLocation, DateTime? bestBefore)
    {
      Guard.Against.Null(resourceLocation, nameof(resourceLocation));

      var addCommand = new AddRouteCommand(_dataProvider);
      var routeId = addCommand.Execute(new AddRouteCommandArgs()
      {
        Url = resourceLocation.ToString(),
        BestBefore = bestBefore,
      });

      return new ShortGuid(routeId).ToString();
    }

    #endregion
  }
}