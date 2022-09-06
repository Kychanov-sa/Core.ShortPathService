using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GlacialBytes.ShortPathService.Persistence.Database
{
  /// <summary>
  /// Инжектор зависимостей.
  /// </summary>
  public static class DependencyInjection
  {
    /// <summary>
    /// Добавляет проверки здоровья сервиса.
    /// </summary>
    /// <param name="builder">Построитель проверок здоровья.</param>
    /// <param name="failureStatus">Статус нарушения проверки.</param>
    /// <param name="tags">Теги проверок.</param>
    /// <returns>Дополненный построитель проверок здоровья.</returns>
    public static IHealthChecksBuilder AddDataContextHealthChecks(
      this IHealthChecksBuilder builder,
      HealthStatus? failureStatus,
      IEnumerable<string> tags)
    {
      builder.AddDbContextCheck<DataContext>("DataContext check", failureStatus, tags);
      return builder;
    }
  }
}
