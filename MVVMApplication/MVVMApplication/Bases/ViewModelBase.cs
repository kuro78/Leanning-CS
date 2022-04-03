using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MVVMApplication.Bases;

/// <summary>
/// ViewModel 베이스
/// </summary>
public abstract class ViewModelBase : ObservableObject
{
    private string _title;
    public string Title
    {
        get { return _title; }
        set { SetProperty(ref _title, value); }
    }
}
