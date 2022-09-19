using System;
using GlacialBytes.Foundation.Diagnostics;
using GlacialBytes.ShortPathService.Domain.Data;
using GlacialBytes.ShortPathService.Domain.Data.Commands;

namespace GlacialBytes.Core.ShortPathService.Services
{
  /// <summary>
  /// Воркер очистки маршрутов.
  /// </summary>
  internal class RouteCleaningWorker : IRouteCleaningWorker
  {
    /// <summary>
    /// Провайдер данных.
    /// </summary>
    private readonly IDataProvider _dataProvider;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="dataProvider">Провайдер данных.</param>
    public RouteCleaningWorker(IDataProvider dataProvider)
    {
      _dataProvider = dataProvider;
    }

    /// <summary>
    /// Удаляет просроченные маршруты.
    /// </summary>
    private void RemoveExpiredRoutes()
    {
      var removeCommand = new RemoveExpiredRoutesCommand(_dataProvider);
      removeCommand.Execute(new RemoveExpiredRoutesCommandArgs()
      {
        ExpiredAfterDate = DateTime.UtcNow,
      });
    }
  }
}
