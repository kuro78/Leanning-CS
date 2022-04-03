using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Windows.Input;
using MVVMApplication.Bases;
using MVVMApplication.Models;


namespace MVVMApplication.ViewModels;

/// <summary>
/// Main ViewModel Class
/// </summary>
public class MainViewModel : ViewModelBase
{
    private string _navigationSource;
    /// <summary>
    /// 네비게이션 소스
    /// </summary>
    public string NavigationSource
    {
        get { return _navigationSource; }
        set { SetProperty(ref _navigationSource, value); }
    }

    /// <summary>
    /// 네비게이트 커맨드
    /// </summary>
    public ICommand NavigateCommand { get; set; }
    
    /// <summary>
    /// Constructor
    /// </summary>
    public MainViewModel()
    {
        Title = "Main View";
        Init();
    }

    private void Init()
    {
        // 시작 페이지 설정
        NavigationSource = "Views/HomePage.xaml";
        NavigateCommand = new RelayCommand<string>(OnNavigate);

        // 네비게이션 메시지 수신 등록
        WeakReferenceMessenger.Default.Register<NavigationMessage>(this, OnNavigationMessage);
    }

    /// <summary>
    /// 네비게이션 메시지 수신 처리
    /// </summary>
    /// <param name="recipent"></param>
    /// <param name="message"></param>
    private void OnNavigationMessage(object recipent, NavigationMessage message)
    {
        NavigationSource = message.Value;
    }

    private void OnNavigate(string pageUri)
    {
        NavigationSource = pageUri;
    }
}
