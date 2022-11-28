using System;
using GlacialBytes.Core.ShortPathService.Services.Exceptions;
using GlacialBytes.Core.ShortPathService.WebApi.Service.Exceptions;
using GlacialBytes.Core.ShortPathService.WebApi.TransferModels;
using GlacialBytes.Foundation.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Controllers
{
  /// <summary>
  /// Контроллер для обработки ошибок.
  /// </summary>
  [ApiExplorerSettings(IgnoreApi = true)]
  [ApiController]
  public class ErrorsController : ControllerBase
  {
    /// <summary>
    /// Метод обрабатывает ошибку, возникающую в сервисах.
    /// </summary>
    /// <param name="exceptionHandler">Обработчик исключений.</param>
    /// <param name="hostEnvironment">Окружение хостинга.</param>
    [Route("/error")]
    public IActionResult HandleError(
      [FromServices] IExceptionHandler exceptionHandler,
      [FromServices] IWebHostEnvironment hostEnvironment)
    {
      var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
      var exception = context?.Error;

      bool isDevelopmentEnvironment = "Development".Equals(hostEnvironment.EnvironmentName);
      if (exception != null)
        return HandleException(exceptionHandler, exception, isDevelopmentEnvironment);

      return new JsonResult(FailureResult.UndefinedError);
    }

    /// <summary>
    /// Обрабатывает исключение, используя доступный в контейнере зависимостей обработчик.
    /// </summary>
    /// <param name="handler">Обработчик исключений.</param>
    /// <param name="exception">Исключение.</param>
    /// <param name="returnDetails">Признак необходимости вернуть детальную информацию по исключению.</param>
    /// <returns>Результат выполнения метода контроллера.</returns>
    public static ActionResult HandleException(
      IExceptionHandler handler,
      Exception exception,
      bool returnDetails = false)
    {
      if (handler != null)
        handler.Handle(exception);

      switch (exception)
      {
        case CriticalException criticalException:
          return new JsonResult(
           new FailureResult
           {
             Reason = criticalException.Message,
             Code = "ERR_CRITICAL",
           });
        case BaseApplicationException baseApplicationException:
          return new JsonResult(
           new FailureResult
           {
             Reason = baseApplicationException.Message,
             Code = baseApplicationException.ErrorCode,
           });
        default:
          return new JsonResult(
            new FailureResult
            {
              Reason = Localization.GetString("ERR_UNEXPECTED_ERROR_OCCURRED"),
              Details = returnDetails ? exception.StackTrace : null,
            });
      }
    }
  }
}
