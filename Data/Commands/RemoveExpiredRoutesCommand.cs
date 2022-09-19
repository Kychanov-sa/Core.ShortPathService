using GlacialBytes.Foundation.Diagnostics;
using GlacialBytes.ShortPathService.Domain.Data.DataModels;

namespace GlacialBytes.ShortPathService.Domain.Data.Commands
{
  /// <summary>
  /// Команда удаления маршрутов с истёкшим новым сроком действия.
  /// </summary>
  public class RemoveExpiredRoutesCommand : GenericCommand<RemoveExpiredRoutesCommandArgs>
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="provider">Провайдер данных.</param>
    public RemoveExpiredRoutesCommand(IDataProvider provider)
      : base(provider)
    {
    }

    #region ICommand<Guid, RemoveExpiredRoutesCommandArgs, int>

    /// <summary>
    /// <see cref="ICommand{TKey, TArgs}.Execute(TArgs)"/>
    /// </summary>
    public override void Execute(RemoveExpiredRoutesCommandArgs args)
    {
      DebugAssert.IsNotNull(args);

      var repository = DataProvider.GetRepository<Route>();
      repository.DeleteMany(r => r.BestBefore != null && r.BestBefore < args.ExpiredAfterDate);
      Commit();
    }
    #endregion
  }
}
