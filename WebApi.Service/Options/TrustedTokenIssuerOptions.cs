using GlacialBytes.Core.ShortPathService.Diagnostics;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Options
{
  /// <summary>
  /// Настройки доверенного издателя токенов.
  /// </summary>
  public class TrustedTokenIssuerOptions
  {
    /// <summary>
    /// Имя издателя токена.
    /// </summary>
    [LoggableOption]
    public string Issuer { get; set; }

    /// <summary>
    /// Ключ шифрования токенов.
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
