using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Настройки культуры.
  /// </summary>
  public class GlobalizationOptions
  {
    /// <summary>
    /// Стандартная культура.
    /// </summary>
    [LoggableOption]
    public string DefaultCulture { get; set; }
  }
}
