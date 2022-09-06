using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GlacialBytes.Foundation.Data;
using GlacialBytes.Foundation.Diagnostics;
using GlacialBytes.ShortPathService.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace GlacialBytes.ShortPathService.Persistence.Database
{
  /// <summary>
  /// Обобщённая реализация репозитория.
  /// </summary>
  /// <typeparam name="T">Тип хранимых моделей.</typeparam>
  internal class GenericRepository<T> : IRepository<T>
      where T : DataModel<Guid>
  {
    /// <summary>
    /// Контекст данных.
    /// </summary>
    private readonly DataContext _context;

    /// <summary>
    /// Набор данных.
    /// </summary>
    private readonly DbSet<T> _set;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="context">Контекст данных.</param>
    public GenericRepository(DataContext context)
    {
      DebugAssert.IsNotNull(context);

      _context = context;
      _set = _context.Set<T>();
    }

    #region IRepository<T>

    /// <summary>
    /// <see cref="IRepository{T, TItem}.Name"/>
    /// </summary>
    public string Name
    {
      get
      {
        var mapping = _context.Model.FindEntityType(typeof(T));
        return mapping.GetTableName();
      }
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.Any(Expression{Func{TItem, bool}})"/>
    /// </summary>
    public bool Any(Expression<Func<T, bool>> where)
    {
      return _set.Any(where);
    }

    /// <inheritdoc/>
    public Task<bool> AnyAsync(Expression<Func<T, bool>> where)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> AnyAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.Create(TItem)"/>
    /// </summary>
    public void Create(T item)
    {
      DebugAssert.IsNotNull(item);
      _set.Add(item);
    }

    /// <inheritdoc/>
    public Task<T> CreateAsync(T item)
    {
      DebugAssert.IsNotNull(item);
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<T> CreateAsync(T item, CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(item);
      DebugAssert.IsNotNull(cancellationToken);
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.CreateMany(IEnumerable{TItem})"/>
    /// </summary>
    public void CreateMany(IEnumerable<T> items)
    {
      DebugAssert.IsNotNull(items);
      _set.AddRange(items);
    }

    /// <inheritdoc/>
    public Task CreateManyAsync(IEnumerable<T> items)
    {
      DebugAssert.IsNotNull(items);
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task CreateManyAsync(IEnumerable<T> items, CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(items);
      DebugAssert.IsNotNull(cancellationToken);
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.Delete(T)"/>
    /// </summary>
    public void Delete(Guid key)
    {
      var deletingObject = _set.FirstOrDefault(e => e.Id == key);
      _set.Remove(deletingObject);
    }

    /// <inheritdoc/>
    public Task DeleteAsync(Guid key)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task DeleteAsync(Guid key, CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(cancellationToken);
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.DeleteMany(Expression{Func{TItem, bool}})"/>
    /// </summary>
    /// <param name="where">Выражение для выборки объектов.</param>
    public int DeleteMany(Expression<Func<T, bool>> where)
    {
      DebugAssert.IsNotNull(where);

      var deletingObjects = _set.Where(where);
      _set.RemoveRange(deletingObjects);
      return deletingObjects.Count();
    }

    /// <inheritdoc/>
    public Task<int> DeleteManyAsync(Expression<Func<T, bool>> where)
    {
      DebugAssert.IsNotNull(where);
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<int> DeleteManyAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(where);
      DebugAssert.IsNotNull(cancellationToken);
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.Get(T)"/>
    /// </summary>
    public T Get(Guid key)
    {
      return _set.SingleOrDefault(e => e.Id == key);
    }

    /// <inheritdoc/>
    public Task<T> GetAsync(Guid key)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<T> GetAsync(Guid key, CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(cancellationToken);
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.GetAll()"/>
    /// </summary>
    public IEnumerable<T> GetAll()
    {
      return _set.ToArray();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.GetAllAsync()"/>
    /// </summary>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
      return await _set.AsNoTracking().ToArrayAsync();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.GetAllAsync(CancellationToken)"/>
    /// </summary>
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(cancellationToken);
      return await _set.AsNoTracking().ToArrayAsync(cancellationToken);
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.GetMany(Expression{Func{TItem, bool}})"/>
    /// </summary>
    public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
    {
      DebugAssert.IsNotNull(where);
      return _set.Where(where);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
    {
      DebugAssert.IsNotNull(where);
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(where);
      DebugAssert.IsNotNull(cancellationToken);
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.GetOne(Expression{Func{TItem, bool}})"/>
    /// </summary>
    public T GetOne(Expression<Func<T, bool>> where)
    {
      DebugAssert.IsNotNull(where);
      return _set.Where(where).SingleOrDefault();
    }

    /// <inheritdoc/>
    public Task<T> GetOneAsync(Expression<Func<T, bool>> where)
    {
      DebugAssert.IsNotNull(where);
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<T> GetOneAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(where);
      DebugAssert.IsNotNull(cancellationToken);
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.GetQuery()"/>
    /// </summary>
    public IQueryable<T> GetQuery()
    {
      return _set.AsQueryable();
    }

    /// <inheritdoc/>
    public Task<IQueryable<T>> GetQueryAsync()
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<IQueryable<T>> GetQueryAsync(CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(cancellationToken);
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.Update(TItem, T)"/>
    /// </summary>
    public void Update(T item, Guid key)
    {
      DebugAssert.IsNotNull(item);
      var current = _set.FirstOrDefault(e => e.Id == key);
      if (current != null)
      {
        _context.Entry(current).CurrentValues.SetValues(item);
      }
    }

    /// <inheritdoc/>
    public Task UpdateAsync(T item, Guid key)
    {
      DebugAssert.IsNotNull(item);
      throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task UpdateAsync(T item, Guid key, CancellationToken cancellationToken)
    {
      DebugAssert.IsNotNull(item);
      DebugAssert.IsNotNull(cancellationToken);
      throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.FullTextSearch{TProperty}(Expression{Func{TItem, TProperty}}, string)"/>
    /// </summary>
    /// <typeparam name="TProperty">Параметр свойств универсального типа.</typeparam>
    public IEnumerable<T> FullTextSearch<TProperty>(Expression<Func<T, TProperty>> propertyExpression, string query)
    {
      // TODO: 2022-04-15 Переделать полнотекстовый поиск без использования MSSQL специфики.
      throw new NotImplementedException();

      // DebugAssert.IsNotNull(propertyExpression);
      // DebugAssert.IsNotNullOrEmpty(query);

      // var propInfo = GetPropertyInfoFromExpression(propertyExpression);
      // return _set.Where(item => EF.Functions.FreeText(EF.Property<string>(item, propInfo.Name), query));
    }

    /// <summary>
    /// <see cref="IRepository{T, TItem}.FullTextSearch{TProperty}(Expression{Func{TItem, TProperty}}, string, Expression{Func{TItem, bool}})"/>
    /// </summary>
    /// <typeparam name="TProperty">Параметр свойств универсального типа.</typeparam>
    public IEnumerable<T> FullTextSearch<TProperty>(Expression<Func<T, TProperty>> propertyExpression, string query, Expression<Func<T, bool>> where)
    {
      // TODO: 2022-04-15 Переделать полнотекстовый поиск без использования MSSQL специфики.
      throw new NotImplementedException();

      // DebugAssert.IsNotNull(propertyExpression);
      // DebugAssert.IsNotNullOrEmpty(query);

      // var propInfo = GetPropertyInfoFromExpression(propertyExpression);
      // return _set.Where(where).Where(item => EF.Functions.FreeText(EF.Property<string>(item, propInfo.Name), query));
    }

    /// <summary>
    /// <see cref="DomainData.IRepository{T}.GetCount(Expression{Func{T, bool}})"/>
    /// </summary>
    public int GetCount(Expression<Func<T, bool>> where)
    {
      return _set.Where(where).Count();
    }

    #endregion

    /// <summary>
    /// Возвращает информацию о свойстве из выражения.
    /// </summary>
    /// <param name="propertyExpression">Выражение свойства.</param>
    private static PropertyInfo GetPropertyInfoFromExpression<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
    {
      if (propertyExpression.Body is not MemberExpression member)
        throw new ArgumentException($"Expression '{propertyExpression}' refers to a method, not a property.");

      PropertyInfo propInfo = member.Member as PropertyInfo;
      if (propInfo == null)
        throw new ArgumentException($"Expression '{propertyExpression}' refers to a field, not a property.");
      return propInfo;
    }
  }
}
