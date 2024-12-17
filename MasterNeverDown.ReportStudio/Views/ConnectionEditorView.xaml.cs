

namespace AakStudio.Shell.UI.Showcase.Views;

public partial class ConnectionEditorView : UserControl
{
    public ConnectionEditorView(ConnectionEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void ValidationErrorHandler(object? sender, ValidationErrorEventArgs e)
    {
        (DataContext as ConnectionEditorViewModel)!.IsSaveEnabled =
            Validation.GetErrors(sender as UIElement ?? throw new InvalidOperationException()).Count <= 0;
    }
}