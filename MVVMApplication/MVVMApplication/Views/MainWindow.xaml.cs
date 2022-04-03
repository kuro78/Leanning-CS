﻿using MVVMApplication.ViewModels;
using System.Windows;

namespace MVVMApplication;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService(typeof(MainViewModel));
    }
}
