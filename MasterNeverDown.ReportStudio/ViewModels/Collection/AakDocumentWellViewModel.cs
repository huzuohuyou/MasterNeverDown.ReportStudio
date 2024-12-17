namespace AakStudio.Shell.UI.Showcase.ViewModels.Collection;

public sealed class AakDocumentWellViewModel : AakDocumentWell
{
    public AakCollectionViewModel Parent { get; }

    public AakDocumentWellViewModel(UIElement view, string title, AakCollectionViewModel parent)
    {
        Parent = parent;
        Title = title;
        View = view;
    }
    public AakDocumentWellViewModel( string title, AakCollectionViewModel parent)
    {
        Parent = parent;
        Title = title;
    }
    protected override void OnActive()
    {
        Parent.ActiveDocument(this);
    }

    protected override void OnClose()
    {
        Parent.CloseTab(this);
    }
}