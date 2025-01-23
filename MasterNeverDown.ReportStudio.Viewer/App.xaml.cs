using System.IO;
using System.Reflection;
using System.Windows.Threading;
using CommunityToolkit.ReportEditor.Model.Models;
using CommunityToolkit.ReportEditor.Shell.Services;
using CommunityToolkit.ReportEditor.Shell.ViewModels.Pages;
using CommunityToolkit.ReportEditor.Shell.ViewModels.Windows;
using CommunityToolkit.ReportEditor.Shell.Views.Pages;
using CommunityToolkit.ReportEditor.Shell.Views.Windows;
using MasterNeverDown.ReportStudio.Viewer.Views.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Wpf.Ui;

namespace CommunityToolkit.ReportEditor.Shell;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
        .ConfigureServices((context, services) =>
        {
            
            services.AddSingleton<DataSourceContext>();
            services.AddHostedService<ApplicationHostService>();
            // services.AddSingleton<WindowsProviderService>();
            // Page resolver service
            services.AddSingleton<IPageService, PageService>();

            // Theme manipulation
            services.AddSingleton<IThemeService, ThemeService>();

            // TaskBar manipulation
            services.AddSingleton<ITaskBarService, TaskBarService>();

            // Service containing navigation, same as INavigationWindow... but without window
            services.AddSingleton<INavigationService, NavigationService>();

            // Main window with navigation
            services.AddSingleton<INavigationWindow, MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<DashboardPage>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<DataPage>();
            services.AddSingleton<DataViewModel>();
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<ReportViewerPage>();
            
            // services.AddSingleton<EditorControlViewModel>();
            // services.AddSingleton<EditorControl>();
            // ConfiguratorModule.AddService(services);
            // var module = new ModuleService(services);
            // // module.AddReferenceAssembly();
            // module.AddService();
        }).Build();

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetService<T>()
        where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}