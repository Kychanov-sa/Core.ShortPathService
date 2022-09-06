using GlacialBytes.Foundation.Localizations;

namespace GlacialBytes.ShortPathService.Persistence.Database
{
  /// <summary>
  /// Локализация для проекта взаимодействия с базой данных.
  /// </summary>
  internal static class Localization
  {
    /// <summary>
    /// Конструктор.
    /// </summary>
    static Localization()
    {
      var localizationProvider = (ResourceLocalizationProvider)LocalizationFactory.Instance.GetProvider(ResourceLocalizationProvider.DefaultPrefix);
      localizationProvider.AddResourceManager("SQL", Properties.Resources.ResourceManager);
    }

    /// <summary>
    /// Возвращает строку локализации.
    /// </summary>
    /// <param name="code">Код строки локализации.</param>
    /// <param name="parameters">Параметры строки.</param>
    /// <returns>Строка локализации.</returns>
    public static LocalizedString GetString(string code, params object[] parameters)
    {
      return new LocalizedString(ResourceLocalizationProvider.DefaultPrefix + "DB." + code, parameters);
    }
  }
}
