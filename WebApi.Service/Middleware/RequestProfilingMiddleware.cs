using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Middleware
{
  /// <summary>
  /// Промежуточное ПО профайлинга запросов.
  /// </summary>
  public class RequestProfilingMiddleware
  {
    /// <summary>Идентификатор запроса.</summary>
    private static volatile int _requestId = 0;

    /// <summary>Следующий в очереди делегат.</summary>
    private readonly RequestDelegate _next;

    /// <summary>Логгер профайлера.</summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="next">Следующий в очереди делегат.</param>
    /// <param name="loggerFactory">Фабрика логгеров.</param>
    public RequestProfilingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
      _next = next;
      _logger = loggerFactory.CreateLogger("PROFILER");
    }

    /// <summary>
    /// Обработчик запроса.
    /// </summary>
    /// <param name="context">Контектс запроса.</param>
    public async Task Invoke(HttpContext context)
    {
      int currentRequestId = Interlocked.Increment(ref _requestId);
      var requestEventId = new EventId(currentRequestId);
      var watch = new Stopwatch();
      try
      {
        watch.Start();
        await _next(context);
      }
      finally
      {
        watch.Stop();

        _logger.LogTrace(
          requestEventId,
          "{method}{url}{statusCode}{duration}",
          context.Request?.Method,
          context.Request?.Path.Value,
          context.Response?.StatusCode,
          watch.ElapsedMilliseconds);
      }
    }
  }
}
