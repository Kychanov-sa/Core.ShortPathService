using System;
using GlacialBytes.Foundation.Data;

namespace GlacialBytes.ShortPathService.Domain.Data.DataModels
{
  /// <summary>
  /// Базовый класс для моделей из хранилища.
  /// </summary>
  public class DbObject : DataModel<Guid>
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    public DbObject()
    {
      Id = Guid.NewGuid();
    }

    /// <summary>
    /// <see cref="object.GetHashCode()"/>
    /// </summary>
    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }
  }
}
