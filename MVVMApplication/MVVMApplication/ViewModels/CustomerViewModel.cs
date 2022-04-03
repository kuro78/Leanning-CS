using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Windows.Input;
using MVVMApplication.Bases;
using MVVMApplication.Models;

namespace MVVMApplication.ViewModels;

public class CustomerViewModel : ViewModelBase
{
    public ICommand BackCommand { get; set; }

    public CustomerViewModel()
    {
        Init();
    }

    private void Init()
    {
        Title = "Customer";
        BackCommand = new RelayCommand(OnBack);
    }

    private void OnBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("GoBack"));
    }

    public override void OnNavigated(object sender, object navigatedEventArgs)
    {
        Message = "Navigated";
    }
}
