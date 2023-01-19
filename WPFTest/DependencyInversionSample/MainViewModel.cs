using DependencyInversionSample.Common;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversionSample
{
    internal class MainViewModel : ObservableObject
    {
        private DynamicResouce _dr;

        public IRelayCommand LanguageChangeCommand { get; set; }

        public MainViewModel()
        {
            _dr = App.Current.Resources["DR"] as DynamicResouce;
            Init();
        }

        private void Init()
        {
            LanguageChangeCommand = new RelayCommand<string>(OnLanguageChangeCommand);
        }

        private void OnLanguageChangeCommand(string para)
        {
            switch(para)
            {
                case "english":
                    _dr.ChangeLanguage("en-us");
                    break;
                case "korean":
                    _dr.ChangeLanguage("ko-kr");
                    break;
            }
        }
    }
}
