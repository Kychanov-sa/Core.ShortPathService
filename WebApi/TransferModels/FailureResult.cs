namespace GlacialBytes.Core.ShortPathService.WebApi.TransferModels
{
  /// <summary>
  /// Модель результата отказа.
  /// </summary>
  public class FailureResult
  {
    /// <summary>
    /// Признак отказа из-за ошибки.
    /// </summary>
    public bool IsError { get; set; } = true;

    /// <summary>
    /// Причина отказа.
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// Детальное описание проблемы.
    /// </summary>
    public string Details { get; set; }

    /// <summary>
    /// Код ошибки.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Неопределённая ошибка.
    /// </summary>
    public static FailureResult UndefinedError
    {
      get
      {
        return new FailureResult
        {
          IsError = true,
          Reason = "Undefined error occurred.",
        };
      }
    }
  }
}
