using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GlacialBytes.Core.ShortPathService.Diagnostics;
using GlacialBytes.Core.ShortPathService.Services;
using GlacialBytes.Core.ShortPathService.WebApi.Service.Exceptions;
using GlacialBytes.Foundation.Dependencies;
using GlacialBytes.ShortPathService.Persistence.Database;
using GlacialBytes.ShortPathService.Persistence.Database.PostgreSql;
using GlacialBytes.ShortPathService.Persistence.Database.SqlServer;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Util;
using static Jaeger.Configuration;

namespace GlacialBytes.Core.ShortPathService.WebApi.Service
{
  /// <summary>
  /// Класс, определяющий процедуру запуска приложения.
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Тип конфигуратора выборки.
    /// https://www.jaegertracing.io/docs/1.26/sampling/
    /// </summary>
    private const string JaegerSamplerType = "const";

    /// <summary>
    /// Конфигурация.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Версия сервиса.
    /// </summary>
    public string ServiceVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

    /// <summary>
    /// Системные теги проверки.
    /// </summary>
    private readonly string[] _systemChecksTags = new[] { "SYSTEM" };

    /// <summary>
    /// Сервисные теги проверки.
    /// </summary>
    private readonly string[] _servicesCheckTags = new[] { "SERVICES" };

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация.</param>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    public void ConfigureServices(IServiceCollection services)
    {
      // Создаём временный провайдер сервисов, который используется подсистемой логирования при запуске приложения
      var temporaryServiceProvider = services.BuildServiceProvider();
      ServiceActivator.Configure(temporaryServiceProvider);

      // Опции
      services.Configure<Options.GeneralOptions>(Configuration.GetSection("General"));
      services.Configure<Options.AuthenticationOptions>(Configuration.GetSection("Authentication"));
      services.Configure<Options.GlobalizationOptions>(Configuration.GetSection("Globalization"));
      services.Configure<Options.DiagnosticsOptions>(Configuration.GetSection("Diagnostics"));
      services.Configure<Options.SecurityOptions>(Configuration.GetSection("Security"));

      // Сервисы контроллеров
      services.AddControllersWithViews(options =>
      {
        options.CacheProfiles.Add(
            "NoCache",
            new CacheProfile()
            {
              NoStore = true,
              Location = ResponseCacheLocation.None,
            });
        options.CacheProfiles.Add(
            "Content",
            new CacheProfile()
            {
              Duration = (int)new TimeSpan(24, 0, 0).TotalSeconds,
              Location = ResponseCacheLocation.Any,
            });
        options.CacheProfiles.Add(
            "Default",
            new CacheProfile()
            {
              Duration = 30,
              Location = ResponseCacheLocation.Any,
            });

        //options.AddAuditFilter(config => { config.LogAllActions().IncludeHeaders(false).IncludeResponseHeaders(false).IncludeResponseBody(false).IncludeRequestBody(false); });
      }).AddControllersAsServices();
      services.AddHttpContextAccessor();

      // Трассировка
      var tracingOptions = Configuration.GetSection("Diagnostics").Get<Options.DiagnosticsOptions>().Tracing;
      AddTracingServices(services, tracingOptions, temporaryServiceProvider);

      // Сервисы достуа к данным
      AddDataProviders(services);

      // Инфраструктура
      AddInfrastructureServices(services);

      // Бизнес логика
      AddApplicationServices(services);

      // Фоновые задачи
      AddBackgroundServices(services);

      // Аутентификация и авторизация
      AddSecurityServices(services);

      // Сервисы проверки здоровья.
      AddHealthChecks(services);

      // Swagger генератор.
      AddSwagger(services);

#if false
      // Логирование конфигурации приложения.
      services.AddSingleton<OptionsLogger>();
#endif
    }

    /// <summary>
    /// Добавляет сервисы трассировки.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="tracingOptions">Опции трассировки.</param>
    /// <param name="serviceProvider">Провайдер сервисов.</param>
    private static void AddTracingServices(
      IServiceCollection services,
      Options.TracingOptions tracingOptions,
      IServiceProvider serviceProvider)
    {
      if (tracingOptions != null && tracingOptions.Enabled)
      {
        var serviceName = !String.IsNullOrWhiteSpace(tracingOptions.ServiceName)
          ? tracingOptions.ServiceName
          : serviceProvider.GetRequiredService<IWebHostEnvironment>().ApplicationName;

        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var senderConfiguration = new SenderConfiguration(loggerFactory)
          .WithAgentHost(tracingOptions.AgentHost)
          .WithAgentPort(tracingOptions.AgentPort)
          .WithSenderResolver(new SenderResolver(loggerFactory).RegisterSenderFactory<ThriftSenderFactory>());
        var reporterConfiguration = new ReporterConfiguration(loggerFactory);
        reporterConfiguration.WithSender(senderConfiguration);

        var samplerConfiguration = new SamplerConfiguration(loggerFactory);
        var tracer = new Jaeger.Configuration(serviceName, loggerFactory)
          .WithReporter(reporterConfiguration)
          .WithSampler(samplerConfiguration.WithType(JaegerSamplerType))
          .GetTracerBuilder()
          .WithSampler(new ConstSampler(sample: true))
          .Build();

        services.AddOpenTracing();
        services.AddSingleton<ITracer>(tracer);
        if (!GlobalTracer.IsRegistered())
        {
          GlobalTracer.Register(tracer);
        }
      }
    }

    /// <summary>
    /// Добавляет в зависимости провайдеры данных.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    private void AddDataProviders(IServiceCollection services)
    {
      var connectionString = GetConnectionString("Database");
      var connectionStringBuilder = new DbConnectionStringBuilder()
      {
        ConnectionString = connectionString,
      };

      var providerName = connectionStringBuilder.ContainsKey("ProviderName") ?
        connectionStringBuilder["ProviderName"].ToString() :
        "System.Data.SqlClient";
      connectionStringBuilder.Remove("ProviderName");

      switch (providerName)
      {
        case "System.Data.SqlClient":
          services.AddSqlServerDataProviders(connectionStringBuilder.ConnectionString);
          break;
        case "Npgsql":
          services.AddPostgreSqlDataContext(connectionStringBuilder.ConnectionString);
          break;
        default:
          throw new InvalidDataProviderException($"Data provider {providerName} is not supported.");
      }
    }

    /// <summary>
    /// Добавляет инфраструктурные сервисы.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private void AddInfrastructureServices(IServiceCollection services)
    {
      services.AddExceptionHandling();
#if false
      services.AddApplicationCache();
#endif
    }

    /// <summary>
    /// Добавляет сервисы приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private void AddApplicationServices(IServiceCollection services)
    {
      services.AddApplicationServices();
    }

    /// <summary>
    /// Добавляет сервисы фоновых задач.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private void AddBackgroundServices(IServiceCollection services)
    {
#if false
      if (!Configuration.GetSection("FacilityFlowProcessingService").Get<FacilityFlowProcessingServiceOptions>().SuspendProcess)
        services.AddHostedService<FacilityFlowProcessingService>();
      services.AddHostedService<CacheValidationService>();
#endif
    }

    /// <summary>
    /// Добавляет сервисы обеспечения безопасности.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private void AddSecurityServices(IServiceCollection services)
    {
      var authenticationOptions = Configuration.GetSection("Authentication").Get<Options.AuthenticationOptions>();
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        SecurityKey securityKey = null;
        try
        {
          if (!String.IsNullOrEmpty(authenticationOptions.SigningCertificatePath))
          {
            var certificate = new X509Certificate2(authenticationOptions.SigningCertificatePath);

            securityKey = new X509SecurityKey(certificate);
          }
          else if (String.IsNullOrEmpty(authenticationOptions.SigningCertificateThumbprint))
          {
            securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationOptions.EncryptionKey));
          }
          else
          {
            var storeLocation = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ?
              StoreLocation.CurrentUser : StoreLocation.LocalMachine;
            using var store = new X509Store(StoreName.My, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, authenticationOptions.SigningCertificateThumbprint, true);
            if (certificates.Count == 0)
              throw new CertificateNotFoundException(authenticationOptions.SigningCertificateThumbprint);
            securityKey = new X509SecurityKey(certificates[0]);
            store.Close();
          }
        }
        catch (Exception ex)
        {
          Log.Exception(ex);
        }

        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidIssuer = authenticationOptions.TrustedIssuer,
          ValidateAudience = true,
          ValidAudience = authenticationOptions.Audience,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = securityKey,
          SaveSigninToken = true,
        };
      });
    }

    /// <summary>
    /// Добавляем проверки здоровья.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private void AddHealthChecks(IServiceCollection services)
    {
      services.AddHealthChecks()
       .AddDataContextHealthChecks(HealthStatus.Unhealthy, _systemChecksTags);
    }

    /// <summary>
    /// Регистрация Swagger генератора.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private void AddSwagger(IServiceCollection services)
    {
      services.AddSwaggerGen(config =>
      {
        config.DocumentFilter<ApiSwaggerFilter>();
        config.ResolveConflictingActions(apiDesc => apiDesc.First());

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        config.IncludeXmlComments(xmlPath);

        config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Description = "Please insert JWT with Bearer into field",
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey,
        });

        var securityScheme = new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer",
          },
        };
        config.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          { securityScheme, Array.Empty<string>() },
        });
      });
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">Построитель приложения.</param>
    /// <param name="env">Окружение хостинга.</param>
    /// <param name="settingsLogger">Логгер конфигурации приложения.</param>
    /// <param name="diagnosticsOptions">Опции диагностики.</param>
    /// <param name="securityOptions">Опции безопасности.</param>
    /// <param name="globalizationOptions">Опции глобализации.</param>
    public void Configure(
      IApplicationBuilder app,
      IWebHostEnvironment env,
      IOptions<Options.DiagnosticsOptions> diagnosticsOptions,
      IOptions<Options.GlobalizationOptions> globalizationOptions,
      IOptions<Options.SecurityOptions> securityOptions)
    {
      ServiceActivator.Configure(app.ApplicationServices);
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "ShortPathService v.1.0"));
      }
      else
      {
        app.UseHsts();
      }

#if false
      if (diagnosticsOptions.Value.EnableRequestProfiling)
      {
        app.UseMiddleware<Middleware.RequestProfilingMiddleware>();
      }
#endif

      app.UseExceptionHandler("/error");
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      // Проверка здоровья
      app.UseHealthChecks("/ready", new HealthCheckOptions() { Predicate = _ => true, ResponseWriter = HealthChecksResponseWriter, });
      app.UseHealthChecks("/health", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("SYSTEM"), ResponseWriter = HealthChecksResponseWriter, });

#if false
      app.UseMiddleware<Middleware.LanguageSetupMiddleware>();
#endif

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapGet("/version", async context =>
        {
          string version = $"{{\"serviceVersion\":\"{ServiceVersion}\"}}";
          context.Response.Headers[HeaderNames.ContentType] = "text/javascript";
          await context.Response.WriteAsync(version);
        });
      });

      // Конфигурируем локализации
      ConfigureLocalizations(globalizationOptions.Value);

#if false
      if (diagnosticsOptions.Value.EnableConfigLogging)
        settingsLogger.LogSettings();
#endif

      // Конфигурируем аудит
      ConfigureAudit(securityOptions.Value);
    }

    /// <summary>
    /// Конфигурирует инфраструктуру аудита запросов.
    /// </summary>
    /// <param name="securityOptions">Опции безопасности.</param>
    private void ConfigureAudit(Options.SecurityOptions securityOptions)
    {
#if false
      Audit.Core.Configuration.Setup()
        .UseNLog(config =>
        {
        });
      Audit.Core.Configuration.AddCustomAction(ActionType.OnEventSaving, action =>
      {
        var webAction = action.Event.GetWebApiAuditAction();
        webAction.FormVariables?.Clear();
        action.Event.Environment.AssemblyName = String.Empty;
        action.Event.Environment.CallingMethodName = String.Empty;

        // HACK: комментарий добавляем к неудачным попыткам, поэтому сбрасываем успешный статус
        if (action.Event.Comments != null && action.Event.Comments.Any())
        {
          webAction.ResponseStatus = "FAILED";
        }
      });
      if (!diagnosticsOptions.EnableAuditLogging)
        Audit.Core.Configuration.AuditDisabled = true;
#endif
    }

    /// <summary>
    /// Конфигурирует подсистему локализации.
    /// </summary>
    /// <param name="globalizationOptions">Опции глобализации.</param>
    private void ConfigureLocalizations(Options.GlobalizationOptions globalizationOptions)
    {
#if false
      try
      {
        var defaultCulture = Thread.CurrentThread.CurrentUICulture;
        var defaultLanguage = defaultCulture.TwoLetterISOLanguageName;

        if (globalizationOptions.Value != null && !String.IsNullOrEmpty(globalizationOptions.Value.DefaultCulture))
        {
          try
          {
            defaultCulture = new CultureInfo(globalizationOptions.Value.DefaultCulture);
            defaultLanguage = defaultCulture.TwoLetterISOLanguageName;
          }
          catch (CultureNotFoundException ex)
          {
            Log.Warning(ex.Message);
          }
        }

        // Устанавливаем культуру для потоков по умолчанию
        CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

        // Инициализируем провайдера локализаций из БД
        using var scope = serviceProvider.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        var dbLocalizationProvider = new DbLocalizationProvider(dataContext)
        {
          DefaultLanguage = defaultLanguage,
        };
        LocalizationFactory.Instance.RegisterProvider(dbLocalizationProvider);
      }
      catch (Exception ex)
      {
        Log.Exception(ex);
      }
#endif
    }

    /// <summary>
    /// Делегат для записи ответа HealthChecks.
    /// </summary>
    /// <param name="context">Http-контекст.</param>
    /// <param name="result">Результат проверки.</param>
    /// <returns>Асинхронная операция.</returns>
    private static Task HealthChecksResponseWriter(HttpContext context, HealthReport result)
    {
      context.Response.ContentType = "application/json";
      return context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }

    /// <summary>
    /// Установка культуры по умолчанию.
    /// </summary>
    private void SetDefaultCulture()
    {
#if false
      var cultureOptions = Configuration.Get<Options.GlobalizationOptions>();
      if (!String.IsNullOrEmpty(cultureOptions.DefaultCulture))
      {
        CultureInfo newCulture = new CultureInfo(cultureOptions.DefaultCulture);

        CultureInfo.DefaultThreadCurrentCulture = newCulture;
        CultureInfo.DefaultThreadCurrentUICulture = newCulture;
      }
#endif
    }

    /// <summary>
    /// Возвращает строку подключения по имени.
    /// </summary>
    /// <param name="name">Имя строки подключения.</param>
    /// <returns>Строка подключения.</returns>
    private string GetConnectionString(string name)
    {
      string connectionString = Configuration.GetConnectionString(name);
      return ResolveConnectionStringReferences(connectionString);
    }

    /// <summary>
    /// Разрешает ссылки в строке подключения, подставляя значения из конфигурации.
    /// </summary>
    /// <param name="connectionString">Строка подключения.</param>
    /// <returns>Строка подключения с подставленными значениями вместо ссылок.</returns>
    private string ResolveConnectionStringReferences(string connectionString)
    {
      if (String.IsNullOrWhiteSpace(connectionString))
        return String.Empty;

      var stringBuilder = new StringBuilder();
      foreach (string connectionStringElement in connectionString.Split(';'))
      {
        if (String.IsNullOrWhiteSpace(connectionStringElement))
          continue;

        string[] keyValuePair = connectionStringElement.Split('=');
        if (keyValuePair.Length > 1 && keyValuePair[1].Length > 2 && keyValuePair[1][0] == '@' && keyValuePair[1][1] != '@')
        {
          keyValuePair[1] = Configuration.GetValue<string>(keyValuePair[1].Substring(1));
        }
        stringBuilder.Append($"{keyValuePair[0]}={keyValuePair[1]};");
      }
      return stringBuilder.ToString();
    }
  }
}
