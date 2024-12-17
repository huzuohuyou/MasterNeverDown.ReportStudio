namespace AakStudio.Shell.UI.Showcase.Views;

public partial class DataSourceEditorView : UserControl
{
    public DataSourceEditorView(DataSourceEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    
    }
}