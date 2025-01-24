using System.Collections.ObjectModel;
using MasterNeverDown.ReportStudio.Viewer.ViewModels.Pages;
using MasterNeverDown.ReportStudio.Viewer.Views.Pages;
using Microsoft.EntityFrameworkCore;
using MenuItem = System.Windows.Controls.MenuItem;

namespace MasterNeverDown.ReportStudio.Viewer.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private string _applicationTitle = "ReportViewer";

    [ObservableProperty] private ObservableCollection<object> _menuItems =
    [
        new NavigationViewItem
        {
            Content = "Home",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
            TargetPageType = typeof(DashboardPage),
        },


        new NavigationViewItem
        {
            Content = "Data",
            Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
            TargetPageType = typeof(DataPage)
        }
    ];

    [ObservableProperty] private ObservableCollection<object> _footerMenuItems =
    [
        new NavigationViewItem
        {
            Content = "Settings",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            TargetPageType = typeof(SettingsPage)
        }
    ];

    [ObservableProperty] private ObservableCollection<MenuItem> _trayMenuItems =
        [new MenuItem { Header = "Home", Tag = "tray_home" }];

    public MainWindowViewModel(DataSourceContext dataSourceContext)
    {
        var reportViewerViewModel = new ReportViewerViewModel(DataSourceContext.Factory);
        (from t in dataSourceContext.Templates select t).ForEachAsync(t =>
        {
            _menuItems.Add(new NavigationViewItem
            {
                Content = t.Name,
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(ReportViewerPage),
                Command = reportViewerViewModel.NavigationItemClickCommand,
                CommandParameter = t.Name
            });
        });
    }

}