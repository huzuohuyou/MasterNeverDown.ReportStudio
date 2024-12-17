namespace AakStudio.Shell.UI.Showcase.Views;

public partial class InputFileNameDialog : Window
{
    public string FileName { get; private set; }

    public InputFileNameDialog()
    {
        InitializeComponent();
    }

    private void OnOkButtonClick(object sender, RoutedEventArgs e)
    {
        FileName = FileNameTextBox.Text;
        DialogResult = true;
    }

    private void OnCancelButtonClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}