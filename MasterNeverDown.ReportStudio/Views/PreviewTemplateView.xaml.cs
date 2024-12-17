using System.ComponentModel;

namespace AakStudio.Shell.UI.Showcase.Views;

public partial class PreviewTemplateView : Window
{
    public PreviewTemplateView(string? templateName)
    {
        InitializeComponent();
        TemplateName.Text = templateName;
    }

    private void Window_Closing(object? sender, CancelEventArgs e)
    {
        // throw new System.NotImplementedException();
    }

    private void okButton_Click(object sender, RoutedEventArgs e) =>
        DialogResult = true;

    private void cancelButton_Click(object sender, RoutedEventArgs e) =>
        DialogResult = false;
}