using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Настройки аутентификации.
  /// </summary>
  public class AuthenticationOptions
  {
    /// <summary>
    /// Доверенные издатели токенов доступа.
    /// </summary>
    [LoggableOption]
    public TrustedTokenIssuerOptions[] TrustedIssuers { get; set; }

    /// <summary>
    /// Имя сервиса.
    /// </summary>
    [LoggableOption]
    public string Audience { get; set; }
  }
}
