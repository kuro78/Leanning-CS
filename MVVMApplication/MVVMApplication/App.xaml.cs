using Microsoft.Extensions.DependencyInjection;
using MVVMApplication.Controls;
using MVVMApplication.Services;
using MVVMApplication.ViewModels;
using System;
using System.Windows;

namespace MVVMApplication;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Register ViewModels
        services.AddTransient(typeof(MainViewModel));
        services.AddTransient(typeof(HomeViewModel));
        services.AddTransient(typeof(CustomerViewModel));
        
        // Register Controls
        services.AddTransient(typeof(AboutControl));

        // Register IDatabaseService SingleTon
        services.AddSingleton<IDatabaseService, SqlService>(obj => new SqlService(connectionString));

        return services.BuildServiceProvider();
    }
}
