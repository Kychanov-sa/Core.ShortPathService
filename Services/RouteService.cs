using System;
using Ardalis.GuardClauses;
using GlacialBytes.ShortPathService.Domain.Data;
using GlacialBytes.ShortPathService.Domain.Data.Commands;
using IdGen;
using Base62;

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
    /// Генератор идентификатров.
    /// </summary>
    private readonly IIdGenerator<long> _idGenerator;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dataProvider">Провайдер данных.</param>
    /// <param name="idGenerator">Генератор идентификатров.</param>
    public RouteService(IDataProvider dataProvider, IIdGenerator<long> idGenerator)
    {
      _dataProvider = dataProvider;
      _idGenerator = idGenerator;
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
        Id = _idGenerator.CreateId(),
        Url = resourceLocation.ToString(),
        BestBefore = bestBefore,
      });

      return routeId.ToBase62();
    }

    #endregion
  }
}