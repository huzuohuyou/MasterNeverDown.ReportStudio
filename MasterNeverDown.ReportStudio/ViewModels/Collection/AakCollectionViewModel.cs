﻿

namespace AakStudio.Shell.UI.Showcase.ViewModels.Collection;

public abstract class AakCollectionViewModel : AakCollection
{
    public AakToolWell Parent { get; }

    public override IEnumerable<UIElement>? Views
    {
        get
        {
            if (Items is null)
            {
                yield break;
            }

            foreach (var item in Items)
            {
                Label linkLabel = new();
                Hyperlink hyperlink = new(new Run(item.Title))
                {
                    Command = new RelayCommand(() =>
                    {
                        item.IsSelected = true;
                        ActiveDocument(item);
                    })
                };
                linkLabel.Content = hyperlink;
                yield return linkLabel;
            }
        }
    }

    public AakCollectionViewModel(AakToolWell parent, string title, string displayName, bool isExpanded = false)
    {
        Parent = parent;

        Title = title;
        IsExpanded = isExpanded;
        DisplayName = displayName;
    }

    private bool isSubItemActive;

    protected override void OnActive()
    {
        if (isSubItemActive)
        {
            isSubItemActive = false;
            return;
        }
        Parent.ActiveDocument(this);
    }

    protected override void OnClose()
    {
        Parent.CloseTab(this);
    }

    internal void ActiveDocument(AakDocumentWell view)
    {
        isSubItemActive = true;
        Parent.ActiveDocument(view);
    }

    internal void CloseTab(AakDocumentWell view)
    {
        Parent.CloseTab(view);
    }
}