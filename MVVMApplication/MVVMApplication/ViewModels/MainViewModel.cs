using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MVVMApplication.Bases;
using MVVMApplication.Models;

namespace MVVMApplication.ViewModels;

/// <summary>
/// Main ViewModel Class
/// </summary>
public class MainViewModel : ViewModelBase
{
    /// <summary>
    /// Busy 목록
    /// </summary>
    private IList<BusyMessage> _busys = new List<BusyMessage>();
    
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

    private bool _isBusy;
    /// <summary>
    /// IsBusy
    /// </summary>
    public bool IsBusy
    {
        get { return _isBusy; }
        set { SetProperty(ref _isBusy, value); }
    }

    private bool _showLayerPopup;
    /// <summary>
    /// Layer Popup 출력 여부
    /// </summary>
    public bool ShowLayerPopup
    {
        get { return _showLayerPopup; }
        set { SetProperty(ref _showLayerPopup, value); }
    }

    private string _controlName;
    public string ControlName
    {
        get { return _controlName; }
        set { SetProperty(ref _controlName, value); }
    }
    
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
        // BusyMessage 수신 등록
        WeakReferenceMessenger.Default.Register<BusyMessage>(this, OnBusyMessage);
        // LayerPopupMessage 수신 등록
        WeakReferenceMessenger.Default.Register<LayerPopupMessage>(this, OnLayerPopupMessage);
    }

    private void OnLayerPopupMessage(object recipient, LayerPopupMessage message)
    {
        ShowLayerPopup = message.Value;
        ControlName = message.ControlName;
    }

    /// <summary>
    /// Budy Message 수신 처리
    /// </summary>
    /// <param name="recipent"></param>
    /// <param name="message"></param>
    private void OnBusyMessage(object recipent, BusyMessage message)
    {
        if(message.Value)
        {
            var existBusy = _busys.FirstOrDefault(b => b.BusyID == message.BusyID);
            if(existBusy != null)
            {
                // 이미 추가된 녀석이기 때문에 추가하지 않음
                return;
            }
            _busys.Add(message);
        }
        else
        {
            var existsBudy = _busys.FirstOrDefault(b => b.BusyID == message.BusyID);
            if(existsBudy == null)
            {
                // 없기 때문에 끝
                return;
            }
            _busys.Remove(existsBudy);
        }
        IsBusy = _busys.Any();
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
