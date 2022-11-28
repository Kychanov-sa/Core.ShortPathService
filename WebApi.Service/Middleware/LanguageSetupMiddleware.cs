using GlacialBytes.Core.ShortPathService.WebApi.Service.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Middleware
{
  /// <summary>
  /// Промежуточное ПО по установке языка.
  /// </summary>
  public class LanguageSetupMiddleware
  {
    /// <summary>Следующий в очереди делегат.</summary>
    private readonly RequestDelegate _next;

    /// <summary>Опции.</summary>
    protected readonly GlobalizationOptions _options;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="next">Следующий в очереди делегат.</param>
    /// <param name="options">Языковые опции.</param>
    public LanguageSetupMiddleware(RequestDelegate next, IOptions<GlobalizationOptions> options)
    {
      _next = next;
      _options = options.Value;
    }

    /// <summary>
    /// Обработчик запроса.
    /// </summary>
    /// <param name="context"0>Контекст запроса.</param>
    public async Task InvokeAsync(HttpContext context)
    {
#if false
      var profile = executionContext.User.Account?.Profile;
      if (profile != null)
      {
        var defaultCulture = Thread.CurrentThread.CurrentUICulture;
        if (!String.IsNullOrEmpty(_options?.DefaultCulture))
        {
          try
          {
            defaultCulture = new CultureInfo(_options.DefaultCulture);
          }
          catch (CultureNotFoundException ex)
          {
            Log.Warning(ex.Message);
          }
        }

        CultureInfo userCulture = null;
        if (!String.IsNullOrEmpty(profile.Language))
        {
          try
          {
            userCulture = new CultureInfo(profile.Language);
          }
          catch (CultureNotFoundException ex)
          {
            Log.Debug(ex.Message);
          }
        }

        // Устанавливаем культуру для потоков по умолчанию
        CultureInfo.DefaultThreadCurrentCulture = userCulture ?? defaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = userCulture ?? defaultCulture;
      }
#endif
      await _next(context);
    }
  }
}
