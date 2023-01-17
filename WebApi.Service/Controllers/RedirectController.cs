using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlacialBytes.Core.ShortPathService.Services;
using GlacialBytes.Core.ShortPathService.Services.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Controllers
{
  /// <summary>
  /// Контроллер перенаправления.
  /// </summary>
  [Controller]
  [Route("")]
  public class RedirectController : Controller
  {
    /// <summary>
    /// Сервис перенаправлений.
    /// </summary>
    private readonly IRedirectionService _redirectionService;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="redirectionService">Сервис перенаправлений.</param>
    public RedirectController(IRedirectionService redirectionService)
    {
      _redirectionService = redirectionService;
    }

    /// <summary>
    /// Перенаправляет по маршруту.
    /// </summary>
    /// <param name="routeId">Идентификатор маршрута.</param>
    /// <returns>Операция перенаправления.</returns>
    [HttpGet("{routeId}")]
    public IActionResult RedirectByRoute(string routeId)
    {
      try
      {
        var redirectionUrl = _redirectionService.GetRedirectUrl(routeId);
        return Redirect(redirectionUrl.ToString());
      }
      catch (BaseApplicationException)
      {
        return NotFound();
      }
    }
  }
}
