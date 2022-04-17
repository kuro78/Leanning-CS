using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThemesSample.Themes;

namespace ThemesSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // loads a png icon, not an ico. 
            Uri iconUri = new Uri("./Resources/idektbh.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ThemesController.IsDarkThemeApplied() is bool isDarkTheme && isDarkTheme)
            {
                ThemesController.CurrentTheme = ThemeTypes.ColourfulDark;
            }
            else
            {
                ThemesController.CurrentTheme = ThemeTypes.ColourfulLight;
            }
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            switch (int.Parse(((MenuItem)sender).Uid))
            {
                case 0:
                    ThemesController.CurrentTheme = ThemeTypes.Light;
                    break;
                case 1:
                    ThemesController.CurrentTheme = ThemeTypes.ColourfulLight;
                    break;
                case 2:
                    ThemesController.CurrentTheme = ThemeTypes.Dark;
                    break;
                case 3:
                    ThemesController.CurrentTheme = ThemeTypes.ColourfulDark;
                    break;
            }

            e.Handled = true;
        }
    }
}
