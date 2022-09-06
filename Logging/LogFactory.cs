using System;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace GlacialBytes.Core.ShortPathService.Diagnostics
{
  /// <summary>
  /// Фабрика логов.
  /// </summary>
  public class LogFactory : IDisposable
  {
    /// <summary>Фабрика логов NLog.</summary>
    private NLog.LogFactory _factory;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configFileName">Имя конфигурационного файла.</param>
    public LogFactory(string configFileName)
    {
      _factory = NLogBuilder.ConfigureNLog(configFileName);
    }

    /// <summary>
    /// Создаёт логгер.
    /// </summary>
    /// <param name="name">Имя логгера.</param>
    /// <returns>Интерфейс логгера.</returns>
    public ILogger CreateLogger(string name)
    {
      return new LogWrapper(_factory.GetLogger(name));
    }

    #region IDisposable

    /// <summary>
    /// <see cref="IDisposable.Dispose"/>
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion

    /// <summary>
    /// Освобождает ресурсы класса.
    /// </summary>
    /// <param name="disposing">Признак необходимости освобождения управляемых ресурсов.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing && _factory != null)
      {
        _factory.Dispose();
        _factory = null;
      }
    }
  }
}
