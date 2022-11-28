namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Exceptions
{
  /// <summary>
  /// Сертификат не найден.
  /// </summary>
  public class CertificateNotFoundException : CriticalException
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="certificateThumbprint">Отпечаток сертификата.</param>
    public CertificateNotFoundException(string certificateThumbprint)
      : base($"Certificate {certificateThumbprint} is not found.")
    {
    }
  }
}
