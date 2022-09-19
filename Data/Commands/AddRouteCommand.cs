using System;
using GlacialBytes.Foundation.Diagnostics;
using GlacialBytes.ShortPathService.Domain.Data.DataModels;

namespace GlacialBytes.ShortPathService.Domain.Data.Commands
{
  /// <summary>
  /// Команда создания нового маршрута.
  /// </summary>
  public class AddRouteCommand : GenericCommand<AddRouteCommandArgs, Guid>
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="provider">Провайдер данных.</param>
    public AddRouteCommand(IDataProvider provider)
      : base(provider)
    {
    }

    #region ICommand<Guid, AddRouteCommandArgs, Guid>

    /// <summary>
    /// <see cref="ICommand{TKey, TArgs, TResult}.Execute(TArgs)"/>
    /// </summary>
    public override Guid Execute(AddRouteCommandArgs args)
    {
      DebugAssert.IsNotNull(args);

      var route = new Route()
      {
        FullUrl = args.Url,
        BestBefore = args.BestBefore,
      };

      var repository = DataProvider.GetRepository<Route>();
      repository.Create(route);

      Commit();
      return route.Id;
    }
    #endregion
  }
}
