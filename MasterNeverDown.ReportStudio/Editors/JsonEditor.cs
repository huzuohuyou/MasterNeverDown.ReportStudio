using System.ComponentModel;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace AakStudio.Shell.UI.Showcase.Editors;

public sealed class JsonEditor : TextEditor, INotifyPropertyChanged
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(JsonEditor),
            new PropertyMetadata((obj, args) =>
            {
                var target = (JsonEditor)obj;
                target.Text = (string)args.NewValue;
            })
        );

    public new string Text
    {
        get { return base.Text; }
        set { base.Text = value; }
    }

    public JsonEditor()
    {
        Options.EnableHyperlinks = false;
        Options.EnableEmailHyperlinks = false; 
        using var stream = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}Editors\\Json.xshd", FileMode.Open);
        using var reader = new System.Xml.XmlTextReader(stream);
        var sqlDefinition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
        HighlightingManager.Instance.RegisterHighlighting("JSON", new[] { ".json" }, sqlDefinition);
        SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("JSON");
        TextArea.TextView.ElementGenerators.Add(new TruncateLongLines());
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