using System;
using System.Threading;
using System.Threading.Tasks;
using GlacialBytes.Foundation.Data;
using GlacialBytes.Foundation.Diagnostics;
using GlacialBytes.Foundation.Domain;

namespace GlacialBytes.ShortPathService.Domain.Data.Commands
{
  /// <summary>
  /// Абстрактный класс единицы работы.
  /// </summary>
  public abstract class UnitOfWork : IUnitOfWork<Guid>
  {
    /// <summary>
    /// Признак, указывающий является ли класс владельцем провайдера данных.
    /// </summary>
    public bool IsDataProviderOwner { get; set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="provider">Провайдер данных.</param>
    public UnitOfWork(IDataProvider provider)
    {
      DebugAssert.IsNotNull(provider);

      DataProvider = provider;
      IsDataProviderOwner = true;
    }

    /// <summary>
    /// Освобождает ресурсы класса.
    /// </summary>
    /// <param name="disposing">Признак необходимости освобождения управляемых ресурсов.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing && IsDataProviderOwner && DataProvider != null)
      {
        DataProvider.Dispose();
      }
    }

    #region IUnitOfWork<Guid>

    /// <summary>
    /// <see cref="IUnitOfWork{TKey}.DataProvider"/>
    /// </summary>
    public IDataProvider<Guid> DataProvider { get; private set; }

    /// <summary>
    /// <see cref="IUnitOfWork{TKey}.Commit"/>
    /// </summary>
    public void Commit()
    {
      DataProvider.SaveChanges();
    }

    /// <summary>
    /// <see cref="IUnitOfWork{TKey}.CommitAsync()"/>
    /// </summary>
    public async Task CommitAsync()
    {
      await DataProvider.SaveChangesAsync();
    }

    /// <summary>
    /// <see cref="IUnitOfWork{TKey}.CommitAsync(CancellationToken)"/>
    /// </summary>
    public Task CommitAsync(CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(cancellationToken);
      return DataProvider.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region

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
