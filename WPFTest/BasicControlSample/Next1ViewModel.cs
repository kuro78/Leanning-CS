using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace BasicControlSample;

public class Next1ViewModel : ObservableObject
{
    private IList<Person> _persons = new ObservableCollection<Person>
    {
        new Person { Name = "kuro7801", Sex = true, Age = 11, Address = "GyeongGi1" },
            new Person { Name = "kuro7802", Sex = false, Age = 12, Address = "GyeongGi2" },
            new Person { Name = "kuro7803", Sex = true, Age = 13, Address = "GyeongGi3" },
            new Person { Name = "kuro7804", Sex = false, Age = 14, Address = "GyeongGi4" },
            new Person { Name = "kuro7805", Sex = true, Age = 15, Address = "GyeongGi5" },
            new Person { Name = "kuro7806", Sex = false, Age = 16, Address = "GyeongGi6" },
            new Person { Name = "kuro7807", Sex = true, Age = 17, Address = "GyeongGi7" },
            new Person { Name = "kuro7808", Sex = false, Age = 18, Address = "GyeongGi8" },
            new Person { Name = "kuro7809", Sex = true, Age = 19, Address = "GyeongGi9" },
            new Person { Name = "kuro7810", Sex = true, Age = 20, Address = "GyeongGi10" },
            new Person { Name = "kuro7811", Sex = false, Age = 21, Address = "GyeongGi11" },
            new Person { Name = "kuro7812", Sex = true, Age = 22, Address = "GyeongGi12" },
            new Person { Name = "kuro7813", Sex = false, Age = 23, Address = "GyeongGi13" },
            new Person { Name = "kuro7814", Sex = true, Age = 24, Address = "GyeongGi14" },
            new Person { Name = "kuro7815", Sex = false, Age = 25, Address = "GyeongGi15" },
            new Person { Name = "kuro7816", Sex = true, Age = 26, Address = "GyeongGi16" },
            new Person { Name = "kuro7817", Sex = false, Age = 27, Address = "GyeongGi17" },
            new Person { Name = "kuro7818", Sex = true, Age = 28, Address = "GyeongGi18" },
            new Person { Name = "kuro7819", Sex = true, Age = 29, Address = "GyeongGi19" },
            new Person { Name = "kuro7820", Sex = false, Age = 30, Address = "GyeongGi20" }
    };
    public IList<Person> Persons { get { return _persons; } }

    public IList<CodeModel> Sexs { get; set; } = new List<CodeModel>
    {
        new CodeModel { Name = "Male", Value = true, Code = "male" },
        new CodeModel { Name = "Female", Value = false, Code = "female" },
    };

    public IList<CodeModel> Addressies { get; set; } = new List<CodeModel>
    {
        new CodeModel { Name = "경기도 수원시", Code="010101" },
        new CodeModel { Name = "경기도 오산시", Code="110110" },
    };

    private Person _selectedListItem;
    public Person SelectedListItem
    {
        get { return _selectedListItem; }
        set { SetProperty(ref _selectedListItem, value); }
    }

    private Person _selectedComboItem;
    public Person SelectedComboItem
    {
        get { return _selectedComboItem; }
        set { SetProperty(ref _selectedComboItem, value); }
    }

    /// <summary>
    /// 리스트 셀렉션 체인지 이벤트 커맨드
    /// </summary>
    public IRelayCommand ListSelectionChangedCommand { get; set; }
    /// <summary>
    /// 리스트 아이템 삭제 커맨드
    /// </summary>
    public IRelayCommand DeleteListItemCommand { get; set; }
    /// <summary>
    /// 콤보 셀렉션 체인지 이벤트 커맨드
    /// </summary>
    public IRelayCommand ComboSelectionChangedCommand { get; set; }
    /// <summary>
    /// 콤보 아이템 삭제 커맨드
    /// </summary>
    public IRelayCommand DeleteComboItemCommand { get; set; }

    public Next1ViewModel()
    {
        Init();
    }

    private void Init()
    {
        ListSelectionChangedCommand = new RelayCommand<object>(OnListSelectionChanged);
        DeleteListItemCommand = new RelayCommand(OnDeleteListIem,
            () => SelectedListItem != null && SelectedListItem.Age % 2 == 0);

        ComboSelectionChangedCommand = new RelayCommand<object>(OnComboSelectionChagned);
        DeleteComboItemCommand = new RelayCommand(OnDeleteComboItem,
            () => SelectedComboItem != null && SelectedComboItem.Age % 2 == 1);
    }

    private void OnDeleteComboItem()
    {
        Persons.Remove(SelectedComboItem);
    }

    private void OnDeleteListIem()
    {
        Persons.Remove(SelectedListItem);
    }

    private void OnComboSelectionChagned(object arg)
    {
        var eventArgs = arg as SelectionChangedEventArgs;
        if (eventArgs == null)
        {
            return;
        }
        Debug.WriteLine($"Combo AddedItems.Count : {eventArgs.AddedItems.Count}");
        Debug.WriteLine($"Combo RemovedItems.Count : {eventArgs.RemovedItems.Count}");

        DeleteComboItemCommand.NotifyCanExecuteChanged();
    }

    private void OnListSelectionChanged(object arg)
    {
        var eventArgs = arg as SelectionChangedEventArgs;
        if(eventArgs == null)
        {
            return;
        }
        Debug.WriteLine($"List AddedItems.Count : {eventArgs.AddedItems.Count}");
        Debug.WriteLine($"List RemovedItems.Count : {eventArgs.RemovedItems.Count}");

        DeleteListItemCommand.NotifyCanExecuteChanged();
    }
}
