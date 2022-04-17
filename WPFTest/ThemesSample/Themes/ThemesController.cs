using Microsoft.Win32;
using System;
using System.Windows;

namespace ThemesSample.Themes;

public enum ThemeTypes
{
    Light,
    ColourfulLight,
    Dark,
    ColourfulDark
}

public static class ThemesController
{
    private static ThemeTypes? _currentTheme = null;
    private static ResourceDictionary _currentThemeDictionary = null;

    public static ThemeTypes? CurrentTheme
    {
        get => _currentTheme;
        set
        {
            string themeName;
            switch(value)
            {
                case ThemeTypes.Dark:
                    themeName = nameof(DarkTheme);
                    break;
                case ThemeTypes.Light:
                    themeName = nameof(LightTheme);
                    break;
                case ThemeTypes.ColourfulDark:
                    themeName = nameof(ColourfulDarkTheme);
                    break;
                case ThemeTypes.ColourfulLight:
                    themeName = nameof(ColourfulLightTheme);
                    break;
                default:
                    return;
            }

            try
            {
                if(_currentThemeDictionary != null)
                {
                    Application.Current.Resources.MergedDictionaries.Remove(_currentThemeDictionary);
                }

                var uri = new Uri($"Themes/{themeName}.xaml", UriKind.Relative);
                _currentThemeDictionary = new ResourceDictionary() { Source = uri };
                Application.Current.Resources.MergedDictionaries.Add(_currentThemeDictionary);
            }
            catch
            { }
        }
    }

    /// <summary>
    /// Registry를 통해 Window에서 라이트/다크 모드 사용 여부 확인'
    /// HKCU\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize의 AppsUseLightTheme 값이 0이면 다크 테마 사용중
    /// htttps://stacoverflow.com/questions/51334674/how-to-detect-windows-10-light-dark-mode-in-wind32-application
    /// </summary>
    /// <returns></returns>
    public static bool? IsDarkThemeApplied()
    {
        using var personalizeKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);
        if(personalizeKey != null && personalizeKey.GetValueKind("AppsUseLightTheme") == RegistryValueKind.DWord)
        {
            return (int)personalizeKey.GetValue("AppsUseLightTheme", 1) == 0;
        }

        return null;
    }
}
