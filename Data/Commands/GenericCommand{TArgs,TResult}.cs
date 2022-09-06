using System;
using GlacialBytes.Foundation.Domain;

namespace GlacialBytes.ShortPathService.Domain.Data.Commands
{
  /// <summary>
  /// Обобщённая команда с результатом.
  /// </summary>
  /// <typeparam name="TArgs">Тип аргументов команды.</typeparam>
  /// <typeparam name="TResult">Тип результата команды.</typeparam>
  public abstract class GenericCommand<TArgs, TResult> : UnitOfWork, ICommand<Guid, TArgs, TResult>
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
    /// <see cref="ICommand{TKey, TArgs, TResult}.Execute(TArgs)"/>
    /// </summary>
    public abstract TResult Execute(TArgs args);
    #endregion
  }
}
