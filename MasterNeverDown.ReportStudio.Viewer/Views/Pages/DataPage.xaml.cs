using CommunityToolkit.ReportEditor.Shell.ViewModels.Pages;
using Wpf.Ui.Controls;

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