using System;
using System.Linq.Expressions;
using GlacialBytes.Foundation.Data;

namespace GlacialBytes.ShortPathService.Domain.Data
{
  /// <summary>
  /// Интерфейс репозитория для хранилища.
  /// </summary>
  /// <typeparam name="T">Тип моделей репозитория.</typeparam>
  public interface IRepository<T> : IRepository<Guid, T>
      where T : DataModel<Guid>
  {
    /// <summary>
    /// Получить количество записей.
    /// </summary>
    /// <param name="where">Выражение фильтра.</param>
    /// <returns>Количество записей.</returns>
    int GetCount(Expression<Func<T, bool>> where);
  }
}
