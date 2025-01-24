using MasterNeverDown.ReportStudio.Viewer.ViewModels.Pages;

namespace MasterNeverDown.ReportStudio.Viewer.Views.Pages;

public partial class DashboardPage : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel { get; }

    public DashboardPage(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}