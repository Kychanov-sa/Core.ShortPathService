using System;
using System.Text;
using GlacialBytes.Foundation.Localizations;

namespace GlacialBytes.ShortPathService.Persistence.Database.Exceptions
{
  /// <summary>
  /// Исключение базы данных.
  /// </summary>
  public class DatabaseException : Exception
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="message">Сообщение исключения.</param>
    public DatabaseException(LocalizedString message)
      : base(message.ToString())
    {
    }

    /// <summary>
    /// Возвращает локализованную строку.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="parameters">Параметры строки локализации.</param>
    /// <returns>Строка локализации.</returns>
    protected static LocalizedString L(string code, params object[] parameters)
    {
      return Localization.GetString(code, parameters);
    }
  }
}
