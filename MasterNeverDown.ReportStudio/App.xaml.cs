using Microsoft.Extensions.DependencyInjection;

namespace AakStudio.Shell.UI.Showcase;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();
        // LanguageManager.CurrentLanguage = new ResourceDictionary { Source = new Uri("Resources\\Resources.en-US.xaml", UriKind.Relative) };
        LangResource.Culture=new CultureInfo("en-US");
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        base.OnStartup(e);
        GlobalExceptionHandler.Register();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Register services and view models
        services.AddTransient<MainWindow>();
        services.AddTransient<ConnectionEditorViewModel>();
        services.AddTransient<DataSourceContext, DataSourceContext>();
    }
}