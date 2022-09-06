using System;
using GlacialBytes.Foundation.Diagnostics;
using Microsoft.Extensions.Logging;

namespace GlacialBytes.Core.ShortPathService.Diagnostics
{
  /// <summary>
  /// Класс обёртка для NLog логгера.
  /// </summary>
  internal class LogWrapper : ILogger
  {
    /// <summary>Логгер NLog.</summary>
    private readonly NLog.Logger _logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="logger">NLog логгер.</param>
    public LogWrapper(NLog.Logger logger)
    {
      DebugAssert.IsNotNull(logger);
      _logger = logger;
    }

    #region ILogger

    /// <summary>
    /// <see cref="ILogger.BeginScope{TState}(TState)"/>
    /// </summary>
    /// <typeparam name="TState">Тип состояния.</typeparam>
    public IDisposable BeginScope<TState>(TState state) => default!;

    /// <summary>
    /// <see cref="ILogger.IsEnabled(LogLevel)"/>
    /// </summary>
    public bool IsEnabled(LogLevel logLevel)
    {
      return logLevel switch
      {
        LogLevel.Debug => _logger.IsDebugEnabled,
        LogLevel.Trace => _logger.IsTraceEnabled,
        LogLevel.Information => _logger.IsInfoEnabled,
        LogLevel.Error => _logger.IsErrorEnabled,
        LogLevel.Warning => _logger.IsWarnEnabled,
        LogLevel.Critical=> _logger.IsFatalEnabled,
        _ => false,
      };
    }

    /// <summary>
    /// <see cref="ILogger.Log{TState}(LogLevel, EventId, TState, Exception, Func{TState, Exception, string})"/>
    /// </summary>
    /// <typeparam name="TState">Тип состояния.</typeparam>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
      if (!IsEnabled(logLevel))
        return;

      var logEvent = new NLog.LogEventInfo()
      {
         Message = formatter(state, exception),
         Level = logLevel switch
         {
           LogLevel.Debug => NLog.LogLevel.Debug,
           LogLevel.Trace => NLog.LogLevel.Trace,
           LogLevel.Information => NLog.LogLevel.Info,
           LogLevel.Error => NLog.LogLevel.Error,
           LogLevel.Warning => NLog.LogLevel.Warn,
           LogLevel.Critical => NLog.LogLevel.Fatal,
           _ => NLog.LogLevel.Info,
         },
         Exception = exception,
      };
      _logger.Log(logEvent);
    }
    #endregion
  }
}
