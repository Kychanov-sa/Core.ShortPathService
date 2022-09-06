using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace GlacialBytes.Core.ShortPathService.Diagnostics
{
  /// <summary>
  /// Класс методов расширений построителей приложений.
  /// </summary>
  public static class DependencyInjection
  {
    /// <summary>
    /// Включает в использование логгер NLog.
    /// </summary>
    /// <param name="builder">Построитель веб-приложения.</param>
    /// <returns>Построитель веб-приложения с добавленным логированием.</returns>
    public static IWebHostBuilder UseLog(this IWebHostBuilder builder)
    {
      return builder.UseNLog();
    }

    /// <summary>
    /// Включает в использование логгер NLog.
    /// </summary>
    /// <param name="builder">Построитель десктоп приложения.</param>
    /// <returns>Построитель веб-приложения с добавленным логированием.</returns>
    public static IHostBuilder UseLog(this IHostBuilder builder)
    {
      return builder.UseNLog();
    }
  }
}
