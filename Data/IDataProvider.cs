using System;
using System.Linq;
using GlacialBytes.Foundation.Data;

namespace GlacialBytes.ShortPathService.Domain.Data
{
  /// <summary>
  /// Интерфейс провайдера данных для хранилища SelfServiceOffice.
  /// </summary>
  public interface IDataProvider : IDataProvider<Guid>
  {
    /// <summary>
    /// Возвращает запрос для выборки объектов.
    /// </summary>
    /// <typeparam name="TItem">Тип выбираемых объектов.</typeparam>
    /// <returns>Интерфейс выборки данных.</returns>
    IQueryable<TItem> GetQuery<TItem>()
      where TItem : class;
  }
}
