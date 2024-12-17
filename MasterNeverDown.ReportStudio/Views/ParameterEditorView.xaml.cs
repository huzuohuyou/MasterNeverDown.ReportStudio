namespace AakStudio.Shell.UI.Showcase.Views;

public partial class ParameterEditorView : UserControl
{
    public ParameterEditorView(ParameterEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataSourceTabControl != null)
        {
            DataSourceTabControl.SelectedIndex = DataSourceComboBox.SelectedIndex+1;
        }
    }
}