using System;
using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Общие опции.
  /// </summary>
  public class GeneralOptions
  {
    /// <summary>
    /// Адрес размещения сервиса.
    /// </summary>
    [LoggableOption]
    public Uri ServiceLocation { get; set; }
  }
}
