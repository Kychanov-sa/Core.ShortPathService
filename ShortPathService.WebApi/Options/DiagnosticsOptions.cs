using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Опции диагностики.
  /// </summary>
  public class DiagnosticsOptions
  {
    /// <summary>
    /// Настройки трассировки.
    /// </summary>
    [LoggableOption]
    public TracingOptions Tracing { get; set; }

    /// <summary>
    /// Настройки профайлинга.
    /// </summary>
    [LoggableOption]
    public ProfilingOptions Profiling { get; set; }

    /// <summary>
    /// Признак логирования опций при запуске приложения.
    /// </summary>
    [LoggableOption]
    public bool LogStartupOptions { get; set; }
  }
}
