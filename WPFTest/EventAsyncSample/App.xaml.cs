﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EventAsyncSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //StartupUri = new Uri("/MainWindow.xaml", UriKind.RelativeOrAbsolute);
            StartupUri = new Uri("/SecondWindow.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
