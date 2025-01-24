using MasterNeverDown.ReportStudio.Model.LangRes;
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
        LangResource.Culture=new CultureInfo( ConfigHelper.GetSelectedLanguage());
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