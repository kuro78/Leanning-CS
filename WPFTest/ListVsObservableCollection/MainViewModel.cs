using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ListVsObservableCollection;

public class MainViewModel : ObservableObject
{
    private IList<Person> _persons = new List<Person>
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

    /// <summary>
    ///  왼쪽 버튼 커맨드
    /// </summary>
    public ICommand LeftButtonCommand { get; set; }
    /// <summary>
    /// 왼쪽 삭제 커맨드
    /// </summary>
    public IRelayCommand LeftRemoveCommand { get; set; }
    /// <summary>
    /// 오른쪽 버튼 커맨드
    /// </summary>
    public ICommand RightButtonCommand { get; set; }
    /// <summary>
    /// 오른쪽 삭제 커맨드
    /// </summary>
    public IRelayCommand RightRemoveCommand { get; set; }

    private Person _selectedLeftPerson;
    /// <summary>
    /// 왼쪽에서 선택된 사람
    /// </summary>
    public Person SelectedLeftPerson
    { 
        get { return _selectedLeftPerson; }
        set { SetProperty(ref _selectedLeftPerson, value); }
    }

    private Person _selectedRightPerson;
    /// <summary>
    /// 오른쪽에서 선택된 사람
    /// </summary>
    public Person SelectedRightPerson
    {
        get { return _selectedRightPerson; }
        set { SetProperty(ref _selectedRightPerson, value); }
    }

    private IList<Person> _leftPeople;
    /// <summary>
    ///  왼쪽 사람들
    /// </summary>
    public IList<Person> LeftPeople
    {
        get { return _leftPeople; }
        set { SetProperty(ref _leftPeople, value); }
    }
    /// <summary>
    /// 왼쪽 타입 네임
    /// </summary>
    public string LeftPeopleTypeName
    {
        get { return LeftPeople.GetType().Name; }
    }

    private IList<Person> _rightPeople = new ObservableCollection<Person>();
    /// <summary>
    /// 오른쪽 사람들
    /// </summary>
    public IList<Person> RightPeople
    {
        get { return _rightPeople; }
        set { SetProperty(ref _rightPeople, value); }
    }
    /// <summary>
    /// 오른쪽 타입 네임
    /// </summary>
    public string RightPeopleTypeName
    {
        get { return RightPeople.GetType().Name; }
    }

    /// <summary>
    /// 생성자
    /// </summary>
    public MainViewModel()
    {
        Init();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    private void Init()
    {
        LeftPeople = _persons;
        ((List<Person>)_persons).ForEach(p => RightPeople.Add(p));

        LeftButtonCommand = new RelayCommand<string>(OnLeftButton);
        RightButtonCommand = new RelayCommand<string>(OnRightButton);

        LeftRemoveCommand = new RelayCommand(() => OnLeftButton("Remove"), () => SelectedLeftPerson != null);
        RightRemoveCommand = new RelayCommand(() => OnRightButton("Remove"), () => SelectedRightPerson != null);

        PropertyChanged += MainViewModel_PropertyChanged;
    }

    private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch(e.PropertyName)
        {
            case nameof(SelectedLeftPerson):
                LeftRemoveCommand.NotifyCanExecuteChanged();
                break;
                case nameof(SelectedRightPerson):
                RightRemoveCommand.NotifyCanExecuteChanged();
                break;
        }
    }

    private void OnRightButton(string parameter)
    {
        switch(parameter)
        {
            case "Refresh":
                RightPeople.Clear();
                foreach(var p in _persons)
                {
                    RightPeople.Add(p);
                }
                break;
            case "Add":
                RightPeople.Insert(0, CreateRandomPerson());
                break;
            case "Remove":
                if(SelectedRightPerson == null)
                {
                    return;
                }
                RightPeople.Remove(SelectedRightPerson);
                break;
        }
    }

    private void OnLeftButton(string parameter)
    {
        switch (parameter)
        {
            case "Refresh":
                LeftPeople = _persons;
                break;
            case "Add":
                //LeftPeople.Insert(0, CreateRandomPerson());
                //OnPropertyChanged(nameof(LeftPeople));
                {
                    var list = LeftPeople.ToList();
                    list.Insert(0, CreateRandomPerson());
                    LeftPeople = list;
                }
                break;
            case "Remove":
                { 
                    if(SelectedLeftPerson == null)
                    {
                        return;
                    }
                    var list = LeftPeople.ToList();
                    list.Remove(SelectedLeftPerson);
                    LeftPeople = list;
                }
                break;
        }
    }

    private Person CreateRandomPerson()
    {
        var random = new Random();
        var randomInt = random.Next(200, 1000);
        return new Person
        {
            Name = $"kuro78{randomInt}",
            Sex = randomInt % 2 == 0,
            Age = randomInt,
            Address = $"Kyeonggi{randomInt}",
        };
    }
}
