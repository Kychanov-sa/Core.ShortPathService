using System;
using GlacialBytes.Foundation.Localizations;

namespace GlacialBytes.Core.ShortPathService.Services.Exceptions
{
  /// <summary>
  /// Базовый класс исключений приложения.
  /// </summary>
  public abstract class BaseApplicationException : Exception
  {
    /// <summary>
    /// Код ошибки.
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    /// Детальная информация по ошибке.
    /// </summary>
    public string Details { get; set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    public BaseApplicationException(LocalizedString message)
      : base(message?.ToString())
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="details">Детальная информация по ошибке.</param>
    public BaseApplicationException(LocalizedString message, LocalizedString details)
      : base(message?.ToString())
    {
      Details = details?.ToString();
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="details">Детальная информация по ошибке.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    public BaseApplicationException(LocalizedString message, LocalizedString details, Exception innerException)
      : base(message?.ToString(), innerException)
    {
      Details = details?.ToString();
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    public BaseApplicationException(LocalizedString message, Exception innerException)
      : base(message?.ToString(), innerException)
    {
    }

    /// <summary>
    /// Возвращает локализованную строку.
    /// </summary>
    /// <param name="messageCode">Код строки локализации.</param>
    /// <param name="parameters">Параметры локализации.</param>
    /// <returns>Локализованная строка.</returns>
    protected static LocalizedString L(string messageCode, params object[] parameters)
    {
      return Localization.GetString(messageCode, parameters);
    }
  }
}
