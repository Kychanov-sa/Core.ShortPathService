using System.Net.Http;
using System.Threading.Tasks;
using GlacialBytes.Core.ShortPathService.WebApi.TransferModels;
using Refit;

namespace GlacialBytes.Core.ShortPathService.WebApi.Client
{
  /// <summary>
  /// Интерфейс API коротких путей.
  /// </summary>
  public interface IShortPathApi
  {
    /// <summary>
    /// Создаёт новый маршрут.
    /// </summary>
    /// <param name="createRoute">Данные нового маршрута.</param>
    /// <param name="authorization">Значение заголовка авторизации.</param>
    /// <returns>Данные созданного маршрута.</returns>
    [Post("/api/routes")]
    Task<Route> CreateRoute([Body] CreateRoute createRoute, [Header("Authorization")] string authorization);

    /// <summary>
    /// Проверка жизнеспособности сервиса.
    /// </summary>
    /// <returns>Ответ на проверку жизнеспособности сервиса.</returns>
    [Get("/health")]
    Task<HttpResponseMessage> HealthCheck();
  }
}
