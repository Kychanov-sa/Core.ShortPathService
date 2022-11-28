using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Настройка профайлинга.
  /// </summary>
  public class ProfilingOptions
  {
    /// <summary>
    /// <c>true</c>, если профайлинг включен.
    /// </summary>
    [LoggableOption]
    public bool Enabled { get; set; }
  }
}
