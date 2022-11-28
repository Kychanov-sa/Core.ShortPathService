namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Exceptions
{
  /// <summary>
  /// Некорректный провайдер данных.
  /// </summary>
  public class InvalidDataProviderException : CriticalException
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="providerName">Имя провайдера.</param>
    public InvalidDataProviderException(string providerName)
      : base($"Data provider {providerName} is not supported.")
    {
    }
  }
}
