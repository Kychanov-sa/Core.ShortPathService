using System;

namespace GlacialBytes.Core.ShortPathService.Diagnostics
{
  /// <summary>
  /// Атрибут, определяющий параметр настроек, который может быть записан в лог.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class LoggableOptionAttribute : Attribute
  {
  }
}
