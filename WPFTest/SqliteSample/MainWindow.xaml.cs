﻿using SQLite;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SqliteSample;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private SQLiteAsyncConnection _db;

    public MainWindow()
    {
        InitializeComponent();
    }

    private async Task InitSqliteAsync()
    {
        // 파일명을 새로 주면 생성하고
        //var databasePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Sample.sqlite");
        // 기존 db 파일을 선택하면 해당 파일을 읽어 옵니다.
        var databasePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Northwind_large.sqlite");
        var db = new SQLiteAsyncConnection(databasePath);
        await db.CreateTableAsync<Customer>();
        _db = db;
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        // select
        await RefreshDataGridAsync();
    }

    /// <summary>
    /// 데이터 조회해서 DataGrid4ㅢ ItemsSource에 넣어서 화면에 출력
    /// </summary>
    /// <returns></returns>
    private async Task RefreshDataGridAsync()
    {
        var customers = await _db.Table<Customer>().ToListAsync();
        DGrid.ItemsSource = customers;
    }

    private async void Button_Click_1(object sender, RoutedEventArgs e)
    {
        // insert
        var newCustomer = new Customer
        {
            Address = "Address",
            City = "City",
            CompanyName = "Company Name",
            ContactName = "Contact Name",
            ContactTitle = "Contact Title",
            Country = "Country",
            Fax = "Fax",
            Id = "kuro78",
            Phone = "Phone",
            PostalCode = "Postal Code",
            REgion = "Region"
        };
        
        var result = await _db.InsertAsync(newCustomer);
        if (result == 0) return;
        
        await RefreshDataGridAsync();
    }

    private async void Button_Click_2(object sender, RoutedEventArgs e)
    {
        // update
        var kuro78 = await _db.FindAsync<Customer>("kuro78");
        if(kuro78 == null) return;
        
        kuro78.CompanyName = "Miro";
        kuro78.Country = "대한민국";
        kuro78.City = "용인시";
        kuro78.Address = "기흥구";
        
        var result = await _db.UpdateAsync(kuro78);
        if(result == 0) return;
        
        await RefreshDataGridAsync();
    }

    private async void Button_Click_3(object sender, RoutedEventArgs e)
    {
        // delete
        var result = await _db.DeleteAsync<Customer>("kuro78");
        if (result == 0) return;

        await RefreshDataGridAsync();
    }

    /// <summary>
    /// 로드
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // 초기화를 시킬 때 await를 사용해야는데, 생성자에서는 비동기호출이 어렵기 때문에 로드 이벤트에서 처리
        await InitSqliteAsync();
    }
}
