namespace AakStudio.Shell.UI.Showcase.Styles;

internal sealed class AakTemplateSelector : DataTemplateSelector
{

    public DataTemplate? AakDocumentWellTemplate
    {
        get;
        set;
    }

    public DataTemplate? AakToolWellTemplate
    {
        get;
        set;
    }

    public DataTemplate? AakCollectionTemplate
    {
        get;
        set;
    }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        if (item is AakCollection)
            return AakCollectionTemplate;

        if (item is AakDocumentWell)
            return AakDocumentWellTemplate;

        if (item is AakToolWell)
            return AakToolWellTemplate;

        return base.SelectTemplate(item, container);
    }
}