using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Windows.Input;
using MVVMApplication.Bases;
using MVVMApplication.Models;
using MVVMApplication.Services;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System;

namespace MVVMApplication.ViewModels;

public partial class CustomerViewModel : ViewModelBase
{
    private readonly IDatabaseService _dbService;

    [ObservableProperty]
    private IList<Customer> _customers;
    [ObservableProperty]
    private Customer _selectedCustomer;
    [ObservableProperty]
    private string _errorMessage;

    public ICommand BackCommand { get; set; }
    public IRelayCommand SaveCommand { get; set; }

    public CustomerViewModel(IDatabaseService databaseService)
    {
        _dbService = databaseService;
        Init();
    }

    private void Init()
    {
        Title = "Customer";
        BackCommand = new RelayCommand(OnBack);
        SaveCommand = new RelayCommand(Save,
                        () => Customers != null
                        && Customers.Any(c => string.IsNullOrWhiteSpace(c?.CustomerID)));

        PropertyChanging += CustomerViewModel_PropertyChanging;
        PropertyChanged += CustomerViewModel_PropertyChanged;
    }

    private void CustomerViewModel_PropertyChanging(object sender, PropertyChangingEventArgs e)
    {
        switch(e.PropertyName)
        {
            case nameof(SelectedCustomer):
                if(SelectedCustomer != null)
                {
                    SelectedCustomer.ErrorsChanged -= SelectedCustomer_ErrorsChanged;
                    ErrorMessage = string.Empty;
                }
                break;
        }
    }

    private void CustomerViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch(e.PropertyName)
        {
            case nameof(SelectedCustomer):
                if(SelectedCustomer != null)
                {
                    SelectedCustomer.ErrorsChanged += SelectedCustomer_ErrorsChanged;
                    SetErrorMessage(SelectedCustomer);
                }
                break;
        }
    }

    private void SelectedCustomer_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
    {
        SetErrorMessage(sender as Customer);
    }

    private void SetErrorMessage(Customer customer)
    {
        if(customer == null)
        {
            return;
        }

        var errors = customer.GetErrors();
        ErrorMessage = string.Join("\n", errors.Select(e => e.ErrorMessage));
    }

    private void Save()
    {
        MessageBox.Show("Save");
        SaveCommand.NotifyCanExecuteChanged();
    }

    private void OnBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage("GoBack"));
    }

    public override async void OnNavigated(object sender, object navigatedEventArgs)
    {
        Message = "Navigated";

        var datas = await _dbService.GetDatasAsync<Customer>("Select * from [Customers]");
        Customers = datas;
    }

    /// <summary>
    /// AddCommand
    /// </summary>
    [ICommand]
    private void Add()
    {
        var newCustomer = new Customer();
        Customers.Insert(0, newCustomer);
        SelectedCustomer = newCustomer;
        SaveCommand.NotifyCanExecuteChanged();
    }
}
