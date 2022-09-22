using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service
{
  /// <summary>
  /// Фильтр документации swagger для API.
  /// </summary>
  public class ApiSwaggerFilter : IDocumentFilter
  {
    /// <summary>
    /// Применяет фильтр.
    /// </summary>
    /// <param name="swaggerDoc">Формируемый документ.</param>
    /// <param name="context">Контекст фильтрации.</param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
      var nonApiRoutes = swaggerDoc.Paths
          .Where(x => !x.Key.ToLower().Contains("/api/"))
          .ToList();
      nonApiRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
    }
  }
}
