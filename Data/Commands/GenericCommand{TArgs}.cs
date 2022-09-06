using System;
using GlacialBytes.Foundation.Domain;

namespace GlacialBytes.ShortPathService.Domain.Data.Commands
{
  /// <summary>
  /// Обобщённая команда без результата.
  /// </summary>
  /// <typeparam name="TArgs">Тип аргументов команды.</typeparam>
  public abstract class GenericCommand<TArgs> : UnitOfWork, ICommand<Guid, TArgs>
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="provider">Провайдер данных.</param>
    public GenericCommand(IDataProvider provider)
      : base(provider)
    {
    }

    #region ICommand<Guid, TArgs, TResult>

    /// <summary>
    /// <see cref="ICommand{TKey, TArgs}.Execute(TArgs)"/>
    /// </summary>
    public abstract void Execute(TArgs args);
    #endregion
  }
}
