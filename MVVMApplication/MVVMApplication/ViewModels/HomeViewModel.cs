using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMApplication.Bases;
using MVVMApplication.Models;

namespace MVVMApplication.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public static int Count { get; set; }

    private decimal _price;
    /// <summary>
    /// 가격
    /// </summary>
    public decimal Price
    {
        get { return _price; }
        set { SetProperty(ref _price, value); }
    }

    /// <summary>
    /// Busy 테스트 커맨드
    /// </summary>
    public ICommand BusyTestCommand { get; set; }

    public ICommand LayerPopupTestCommand { get; set; }

    /// <summary>
    /// constructor
    /// </summary>
    public HomeViewModel()
    {
        Title = "Home";
        Init();
    }
    
    private void Init()
    {
        BusyTestCommand = new AsyncRelayCommand(OnBusyTestAsync);
        LayerPopupTestCommand = new RelayCommand(OnLayerPopupTest);

        Price = 12345678;
    }

    private void OnLayerPopupTest()
    {
        WeakReferenceMessenger.Default.Send(new LayerPopupMessage(true) { ControlName = "AboutControl" });
    }

    /// <summary>
    /// OnBusyTest
    /// </summary>
    /// <returns></returns>
    private async Task OnBusyTestAsync()
    {
        WeakReferenceMessenger.Default.Send(new BusyMessage(true) { BusyID = "OnBusyTestAsync" });
        await Task.Delay(5000);
        WeakReferenceMessenger.Default.Send(new BusyMessage(false) { BusyID = "OnBusyTestAsync" });
    }

    public override void OnNavigated(object sender, object navigatedEventArgs)
    {
        Count++;
        Message = $"{Count} Navigated";
    }
}
