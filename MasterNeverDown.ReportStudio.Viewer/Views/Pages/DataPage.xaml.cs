using DataViewModel = MasterNeverDown.ReportStudio.Viewer.ViewModels.Pages.DataViewModel;

namespace MasterNeverDown.ReportStudio.Viewer.Views.Pages;

public partial class DataPage : INavigableView<DataViewModel>
{
    public DataViewModel ViewModel { get; }

    public DataPage(DataViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}