using System.Collections.ObjectModel;
using CommunityToolkit.ReportEditor.Model.Models;
using MasterNeverDown.ReportStudio.Viewer.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui.Controls;

namespace CommunityToolkit.ReportEditor.Shell.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _applicationTitle = "ReportViewer";

    private readonly DataSourceContext _dataSourceContext;
      
    [ObservableProperty]
    private ObservableCollection<object> _menuItems = new()
    {
       
        new NavigationViewItem
        {
            Content = "Home",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
            TargetPageType = typeof(Views.Pages.DashboardPage),
               
        },


    new NavigationViewItem
        {
            Content = "Data",
            Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
            TargetPageType = typeof(DataPage)
        },
        // new NavigationViewItem
        // {
        //     Content = "Editor",
        //     Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
        //     TargetPageType = typeof(EditorControl)
        // }
    };

    [ObservableProperty]
    private ObservableCollection<object> _footerMenuItems =
    [
        new NavigationViewItem
        {
            Content = "Settings",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            TargetPageType = typeof(Views.Pages.SettingsPage)
        }
    ];

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = [new MenuItem { Header = "Home", Tag = "tray_home" }];

    public MainWindowViewModel(DataSourceContext dataSourceContext)
    {
        _dataSourceContext = dataSourceContext;
       (from t in _dataSourceContext.Templates select t).ForEachAsync(t =>
       {
           _menuItems.Add(new NavigationViewItem()
           {
               Content = t.Name,
               Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
               TargetPageType = typeof(ReportViewerPage)
           });
       });
    }
}