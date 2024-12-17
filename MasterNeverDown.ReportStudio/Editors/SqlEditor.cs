using System.ComponentModel;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace AakStudio.Shell.UI.Showcase.Editors;

public sealed class SqlEditor : TextEditor, INotifyPropertyChanged
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(SqlEditor),
            new PropertyMetadata((obj, args) =>
            {
                var target = (SqlEditor)obj;
                target.Text = (string)args.NewValue;
            })
        );

    public new string Text
    {
        get { return base.Text; }
        set { base.Text = value; }
    }

    public SqlEditor()
    {
        using var stream = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}Editors\\sql.xshd", FileMode.Open);
        using var reader = new System.Xml.XmlTextReader(stream);
        var sqlDefinition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
        HighlightingManager.Instance.RegisterHighlighting("SQL", new[] { ".sql" }, sqlDefinition);
        SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("SQL");
    }

    protected override void OnTextChanged(EventArgs e)
    {
        RaisePropertyChanged("Text");
        base.OnTextChanged(e);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged(string info)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(info));
    }
}