using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Опции безопасности.
  /// </summary>
  public class SecurityOptions
  {
    /// <summary>
    /// Признак включения ведения журнала аудита безопасности.
    /// </summary>
    [LoggableOption]
    public bool EnableAuditLogging { get; set; }
  }
}
