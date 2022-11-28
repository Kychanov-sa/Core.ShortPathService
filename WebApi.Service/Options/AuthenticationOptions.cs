using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Настройки аутентификации.
  /// </summary>
  public class AuthenticationOptions
  {
    /// <summary>
    /// Доверенный издатель токена.
    /// </summary>
    [LoggableOption]
    public string TrustedIssuer { get; set; }

    /// <summary>
    /// Имя сервиса.
    /// </summary>
    [LoggableOption]
    public string Audience { get; set; }

    /// <summary>
    /// Ключ шифрования.
    /// </summary>
    public string EncryptionKey { get; set; }

    /// <summary>
    /// Отпечаток сертификата для проверки аутентичности токенов безопасности.
    /// </summary>
    [LoggableOption]
    public string SigningCertificateThumbprint { get; set; }

    /// <summary>
    /// Путь к файлу *.crt сертификата для проверки аутентичности токенов безопасности.
    /// </summary>
    [LoggableOption]
    public string SigningCertificatePath { get; set; }
  }
}
