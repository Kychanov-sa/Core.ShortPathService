using System;
using System.IO;
using System.Threading;
using GlacialBytes.Core.ShortPathService.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service
{
  /// <summary>
  /// Базовый класс приложения.
  /// </summary>
  public class Program
  {
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    /// <param name="args">Параметры запуска.</param>
    public static void Main(string[] args)
    {
      var configuringFileName = "nlog.config";
      var aspnetEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      var environmentSpecificLogFileName = $"nlog.{aspnetEnvironment}.config";
      if (File.Exists(environmentSpecificLogFileName))
        configuringFileName = environmentSpecificLogFileName;

      using var logFactory = new LogFactory(configuringFileName);
      var applicationLogger = logFactory.CreateLogger("HOST");
      try
      {
        applicationLogger.LogInformation("Application starting...");
        applicationLogger.LogInformation($"Current culture: {Thread.CurrentThread.CurrentCulture.Name}");
        applicationLogger.LogInformation($"Current timezone: {TimeZoneInfo.Local.StandardName}");
        applicationLogger.LogInformation($"Processor count: {Environment.ProcessorCount}");
        applicationLogger.LogInformation($"OS version: {Environment.OSVersion}");
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        applicationLogger.LogError(ex, ex.Message);
        throw;
      }
      finally
      {
        applicationLogger.LogInformation("Application shutted down.");
      }
    }

    /// <summary>
    /// Создает построитель веб-приложения.
    /// </summary>
    /// <param name="args">Аргументы.</param>
    /// <returns>Построитель веб-приложения.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
      .ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      })
      .ConfigureLogging(logging =>
      {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
      })
      .UseLog();
  }
}
