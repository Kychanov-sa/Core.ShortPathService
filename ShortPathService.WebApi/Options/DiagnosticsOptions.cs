using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Опции диагностики.
  /// </summary>
  public class DiagnosticsOptions
  {
    /// <summary>
    ///  Признак включения профайлинга запросов
    /// </summary>
    [LoggableOption]
    public bool EnableRequestProfiling { get; set; }

    /// <summary>
    /// Признак логирования опций при запуске приложения.
    /// </summary>
    [LoggableOption]
    public bool LogStartupOptions { get; set; }
  }
}
