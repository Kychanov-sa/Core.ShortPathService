﻿using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Refit;

[assembly: InternalsVisibleTo("WebApi.Client.Test, PublicKey=00240000048000009400000006020000002400005253413100040000010001002500514a607b230e5731be00c334ce21fa0fcc7ab67c5b4df498c9eadc36375189a4c310222999dd227c6a606b715b7467d2d6d288bae7342272721142c3dd38d0ea89f27168df91ad18998ca6a6d9bbb992495f4160ccfdda6a3fb7ae654792c9c0859bce10342670c23d2992cd35c6377aa8efd74b868003ae5dc017117edc")]
namespace ShortPathService.WebApi.WebApi.Client
{
  /// <summary>
  /// Инжектор зависимостей.
  /// </summary>
  public static class DependencyInjection
  {
    /// <summary>
    /// Добавляет в зависимости сервис сообщений.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    /// <returns>Построитель HTTP клиента.</returns>
    public static IHttpClientBuilder AddShortPathApi(this IServiceCollection services)
    {
      return services.AddRefitClient<IShortPathApi>(new RefitSettings()
      {
      });
    }
  }
}
