using System.Windows;

namespace BasicControlSample;

/// <summary>
/// Next1Window.xaml에 대한 상호 작용 논리
/// </summary>
public partial class Next1Window : Window
{
    public Next1Window()
    {
        InitializeComponent();
        DataContext = new Next1ViewModel();
    }
}
