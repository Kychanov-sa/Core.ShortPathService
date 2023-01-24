using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GlacialBytes.Foundation.Data;
using GlacialBytes.Foundation.Diagnostics;
using GlacialBytes.ShortPathService.Domain.Data;

namespace GlacialBytes.ShortPathService.Persistence.Database
{
  /// <summary>
  /// Провайдер данных.
  /// </summary>
  internal class DataProvider : IDataProvider
  {
    /// <summary>
    /// Контекст данных.
    /// </summary>
    private readonly DataContext _context;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="context">Контекст данных.</param>
    public DataProvider(DataContext context)
    {
      DebugAssert.IsNotNull(context);

      _context = context;
    }

    /// <summary>
    /// Освобождает ресурсы класса.
    /// </summary>
    /// <param name="disposing">Признак необходимости освобождения управляемых ресурсов.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing && _context != null)
      {
        _context.Dispose();
      }
    }

    #region IDataProvider

    /// <summary>
    /// <see cref="IDataProvider{TKey}.GetRepository{TItem}"/>
    /// </summary>
    /// <typeparam name="TItem">Тип хранимых данных.</typeparam>
    public IRepository<long, TItem> GetRepository<TItem>()
        where TItem : DataModel<long>
    {
      return new GenericRepository<TItem>(_context);
    }

    /// <summary>
    /// <see cref="IDataProvider{TKey}.SaveChanges()"/>
    /// </summary>
    public int SaveChanges()
    {
      return _context.SaveChanges();
    }

    /// <summary>
    /// <see cref="IDataProvider{Tkey}.SaveChangesAsync()"/>
    /// </summary>
    public Task<int> SaveChangesAsync()
    {
      return _context.SaveChangesAsync();
    }

    /// <summary>
    /// <see cref="IDataProvider{TKey}.SaveChangesAsync(CancellationToken)"/>
    /// </summary>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
      return _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// <see cref="IDataProvider.GetQuery{TItem}"/>
    /// </summary>
    /// <typeparam name="TItem">Тип хранимых данных.</typeparam>
    public IQueryable<TItem> GetQuery<TItem>()
      where TItem : class
    {
      return _context.Set<TItem>();
    }

    #endregion

    #region IDisposable

    /// <summary>
    /// <see cref="IDisposable.Dispose()"/>
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}
