using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Настройка трассировки.
  /// </summary>
  public class TracingOptions
  {
    /// <summary>
    /// <c>true</c>, если трассировка включена.
    /// </summary>
    [LoggableOption]
    public bool Enabled { get; set; }

    /// <summary>
    /// Название сервиса.
    /// </summary>
    [LoggableOption]
    public string ServiceName { get; set; }

    /// <summary>
    /// Адрес агента сборщика трассировки.
    /// </summary>
    [LoggableOption]
    public string AgentHost { get; set; }

    /// <summary>
    /// Порт агента сборщика трассировки.
    /// </summary>
    [LoggableOption]
    public int AgentPort { get; set; }
  }
}
