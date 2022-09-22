using System;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Exceptions
{
  /// <summary>
  /// Критическое исключение.
  /// </summary>
  public class CriticalException : Exception
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    public CriticalException(string message)
      : base(message)
    {
    }
  }
}
