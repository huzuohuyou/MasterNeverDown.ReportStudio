using System.Collections.ObjectModel;
using System.Windows.Input;
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
    public ICommand NavigationItemClickCommand { get; }
    public MainWindowViewModel(DataSourceContext dataSourceContext)
    {
        NavigationItemClickCommand = new RelayCommand<string>(OnNavigationItemClick);

        (from t in dataSourceContext.Templates select t).ForEachAsync(t =>
        {
            _menuItems.Add(new NavigationViewItem
            {
                Content = t.Name,
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(ReportViewerPage),
                Command = NavigationItemClickCommand,
                CommandParameter = t.Name
            });
        });
    }
    private void OnNavigationItemClick(string itemName)
    {
        ReportViewerViewModel.TemplateName = itemName;
    }
}