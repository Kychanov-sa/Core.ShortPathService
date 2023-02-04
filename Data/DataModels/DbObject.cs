using System;
using GlacialBytes.Foundation.Data;

namespace GlacialBytes.ShortPathService.Domain.Data.DataModels
{
  /// <summary>
  /// Базовый класс для моделей из хранилища.
  /// </summary>
  public class DbObject : DataModel<long>
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    public DbObject()
    {
      Id = -1;
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
