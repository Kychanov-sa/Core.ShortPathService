using System;
using GlacialBytes.Core.ShortPathService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using GlacialBytes.Core.ShortPathService.WebApi.TransferModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service.Controllers.Api
{
  /// <summary>
  /// Контроллер API для работы с маршрутами.
  /// </summary>
  [ApiController]
  [Route("api/routes")]
  public class RoutesController : ControllerBase
  {
    /// <summary>
    /// Сервис маршрутов.
    /// </summary>
    private readonly IRouteService _routeService;

    /// <summary>
    /// Базовый адрес размещения сервиса.
    /// </summary>
    private readonly Uri _serviceBaseAddress;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="routeService">Сервис маршрутов.</param>\
    /// <param name="generalOptions">Общие опции сервиса.</param>
    public RoutesController(IRouteService routeService, IOptions<Options.GeneralOptions> generalOptions)
    {
      _routeService = routeService;
      _serviceBaseAddress = generalOptions.Value.ServiceLocation;
    }

    /// <summary>
    /// Создаёт новый маршрут.
    /// </summary>
    /// <param name="createRoute">Данные нового маршрута.</param>
    /// <returns>Данные созданного маршрута.</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "service")]
    public ActionResult<Route> Post([FromBody] CreateRoute createRoute)
    {
      // TODO: добавить валидацию модели.
      string routeId = _routeService.AddRoute(new Uri(createRoute.Url, UriKind.Absolute), createRoute.BestBefore);
      var route = new Route
      {
        Id = routeId,
        BestBefore = createRoute.BestBefore,
        Url = new Uri(_serviceBaseAddress, routeId).ToString(),
      };

      return Ok(route);
    }
  }
}
