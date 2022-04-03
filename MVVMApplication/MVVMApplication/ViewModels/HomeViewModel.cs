using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using MVVMApplication.Bases;

namespace MVVMApplication.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public static int Count { get; set; }

    public HomeViewModel()
    {
        Title = "Home";
    }

    public override void OnNavigated(object sender, object navigatedEventArgs)
    {
        Count++;
        Message = $"{Count} Navigated";
    }
}
