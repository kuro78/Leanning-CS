using System.Windows;

namespace LargeFileReadSample;

/// <summary>
/// MvvmWindow.xaml에 대한 상호 작용 논리
/// </summary>
public partial class MvvmWindow : Window
{
    public MvvmWindow()
    {
        InitializeComponent();
        DataContext = new MvvmViewModel();
    }
}
