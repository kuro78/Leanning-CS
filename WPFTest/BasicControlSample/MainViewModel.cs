using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BasicControlSample
{
    public class MainViewModel : ObservableObject
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

        private Person _selectedListItem2;
        public Person SelectedListItem2
        {
            get { return _selectedListItem2; }
            set { SetProperty(ref _selectedListItem2, value); }
        }
        public IRelayCommand DeleteListItemCommand { get; set; }

        private Person _selectedComboItem2;
        public Person SelectedComboItem2
        {
            get { return _selectedComboItem2; }
            set { SetProperty(ref _selectedComboItem2, value); }
        }
        public IRelayCommand DeleteComboItemCommand { get; set; }

        public IRelayCommand ShowNextWindowCommand { get; set; }

        public MainViewModel()
        {
            Init();
        }

        private void Init()
        {
            SelectedListItem = Persons.FirstOrDefault();
            SelectedComboItem = Persons.FirstOrDefault();
            // 커맨드 생성
            DeleteListItemCommand = new RelayCommand(OnDeleteListItem,
                () => SelectedListItem2 != null && SelectedListItem2.Age % 2 == 0);
            DeleteComboItemCommand = new RelayCommand(OnDeleteComboItem,
                () => SelectedComboItem2 != null && SelectedComboItem2.Age % 2 == 1);

            ShowNextWindowCommand = new RelayCommand(OnShowNextWindow);

            // 뷰모델 내부에서 프로퍼티 체인지 이벤트 핸들러 추가
            PropertyChanged += MainViewModel_PropertyChanged;
        }

        private void OnShowNextWindow()
        {
            //Sample코드이기 때문에 뷰를 직접 생성해서 Show를 하는 것입니다.
            //실제 애플리케이션일 때 뷰모델에서 뷰를 생성하는 것은 하지 않습니다.
            var nextWindow = new Next1Window();
            nextWindow.Show();
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // nameof를 이용하면, 프로퍼티의 이름이 변경되었을 때 에러가
            // 발생하기 때문에, 에러가 발생하는 것을 방지할 수 있음
            switch(e.PropertyName)
            {
                case nameof(SelectedListItem2):
                    // 커맨드의 사용가능 여부 확인
                    DeleteListItemCommand.NotifyCanExecuteChanged();
                    break;
                case nameof(SelectedComboItem2):
                    DeleteComboItemCommand.NotifyCanExecuteChanged();
                    break;
            }
        }

        private void OnDeleteListItem()
        {
            Persons.Remove(SelectedListItem2);
        }

        private void OnDeleteComboItem()
        {
            Persons.Remove(SelectedComboItem2);
        }
    }
}
