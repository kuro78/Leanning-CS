using Microsoft.Extensions.DependencyInjection;
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

        // Register ViewModels
        services.AddTransient(typeof(MainViewModel));
        services.AddTransient(typeof(HomeViewModel));
        services.AddTransient(typeof(CustomerViewModel));

        return services.BuildServiceProvider();
    }
}
