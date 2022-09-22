using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GlacialBytes.Foundation.Diagnostics;
using GlacialBytes.Foundation.Utilities;
using Microsoft.Extensions.Logging;

namespace GlacialBytes.Core.ShortPathService.Diagnostics
{
  /// <summary>
  /// Обработчик исключений.
  /// </summary>
  internal class ExceptionHandler : IExceptionHandler
  {
    /// <summary>
    /// Логгер.
    /// </summary>
    private ILogger _logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="loggerFactory">Фабрика лога.</param>
    public ExceptionHandler(ILoggerFactory loggerFactory)
    {
      _logger = loggerFactory.CreateLogger("SERVER");
    }

    #region IExceptionHandler

    /// <summary>
    /// <see cref="IExceptionHandler.Handle(Exception)"/>
    /// </summary>
    public bool Handle(Exception exception)
    {
      DebugAssert.IsNotNull(exception);
      return Handle(exception, ExceptionSeverity.Error);
    }

    /// <summary>
    /// <see cref="IExceptionHandler.Handle(Exception, ExceptionSeverity)"/>
    /// </summary>
    public bool Handle(Exception exception, ExceptionSeverity severity)
    {
      DebugAssert.IsNotNull(exception);
      if (exception.InnerException != null)
        Handle(exception.InnerException);
      int errorCode = GetExceptionErrorCode(exception);
      _logger.LogError(exception, exception.Message, errorCode);
      return true;
    }
    #endregion

    /// <summary>
    /// Возвращает код ошибки по исключению.
    /// </summary>
    /// <param name="exception">Объект исключения.</param>
    /// <returns>Код ошибки.</returns>
    private int GetExceptionErrorCode(Exception exception)
    {
      var errorTextBuilder = new StringBuilder();
      errorTextBuilder.AppendLine(exception.GetType().ToString());
      errorTextBuilder.AppendLine(exception.Message);
      errorTextBuilder.AppendLine(GetStackPureSnapshot());
      return Crc32.Calculate(errorTextBuilder.ToString());
    }

    /// <summary>
    /// Возвращает снимок стека вызовов.
    /// </summary>
    /// <returns>Описание стека вызова.</returns>
    private static string GetStackPureSnapshot()
    {
      var stack = new StackTrace();
      var stackMethods = from frame in stack.GetFrames()
                         let method = frame.GetMethod()
                         where method != null && method.Name != "GetStackSnapshot"
                         let methodClass = method.ReflectedType
                         where methodClass != null && methodClass.Name != "ExceptionHandler"
                         let classNamesapce = methodClass.Namespace
                         select $"{classNamesapce}.{methodClass.Name}.{method.Name}";
      return String.Join(";", stackMethods.ToArray());
    }
  }
}
