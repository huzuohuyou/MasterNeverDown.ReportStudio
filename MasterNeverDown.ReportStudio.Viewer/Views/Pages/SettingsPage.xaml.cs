using MasterNeverDown.ReportStudio.Viewer.ViewModels.Pages;

namespace MasterNeverDown.ReportStudio.Viewer.Views.Pages;

public partial class SettingsPage : INavigableView<SettingsViewModel>
{
    public SettingsViewModel ViewModel { get; }

    public SettingsPage(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}