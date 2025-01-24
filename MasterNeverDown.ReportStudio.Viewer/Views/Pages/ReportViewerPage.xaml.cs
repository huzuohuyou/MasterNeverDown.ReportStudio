using MasterNeverDown.ReportStudio.Viewer.ViewModels.Pages;
using DataViewModel = MasterNeverDown.ReportStudio.Viewer.ViewModels.Pages.ReportViewerViewModel;

namespace MasterNeverDown.ReportStudio.Viewer.Views.Pages;

public partial class ReportViewerPage : INavigableView<DataViewModel>
{
    public ReportViewerViewModel ViewModel { get; }

    public ReportViewerPage(ReportViewerViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        InitializeComponent();
    }
}