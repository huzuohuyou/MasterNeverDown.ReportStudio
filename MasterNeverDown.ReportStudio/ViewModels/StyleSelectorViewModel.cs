namespace AakStudio.Shell.UI.Showcase.ViewModels;

public sealed class StyleSelectorViewModel : AakToolWell
{
    public ObservableCollection<AakCollectionViewModel> Collections
    {
        get => collections;
        set => SetProperty(ref collections, value);
    }

    protected override void DeleteEntityByName()
    {
        throw new NotImplementedException();
    }

    public StyleSelectorViewModel(WorkSpaceViewModel workSpaceViewModel)
    {
        this.workSpaceViewModel = workSpaceViewModel;
        var b = new BasicCollectionViewModel(this);
        b.Items= new ObservableCollection<AakDocumentWell>
        {
            new AakDocumentWellViewModel(new ButtonView(), "Button", b),
            new AakDocumentWellViewModel(new SqlView(), "Sql", b),
            new AakDocumentWellViewModel(new CheckBoxView(), "CheckBox", b),
            new AakDocumentWellViewModel(new ComboBoxView(), "ComboBox", b),
            new AakDocumentWellViewModel(new ContextMenuView(), "ContextMenu", b),
            new AakDocumentWellViewModel(new ExpanderView(), "Expander", b),
            new AakDocumentWellViewModel(new GridSplitterView(), "GridSplitter", b),
            new AakDocumentWellViewModel(new GroupBoxView(), "GroupBox", b),
            new AakDocumentWellViewModel(new HyperlinkView(), "Hyperlink", b),
            new AakDocumentWellViewModel(new LabelView(), "Label", b),
            new AakDocumentWellViewModel(new ListBoxView(), "ListBox", b),
            new AakDocumentWellViewModel(new ListViewView(), "ListView", b),
            new AakDocumentWellViewModel(new MenuView(), "Menu", b),
            new AakDocumentWellViewModel(new PasswordBoxView(), "PasswordBox", b),
            new AakDocumentWellViewModel(new ProgressBarView(), "ProgressBar", b),
            new AakDocumentWellViewModel(new ScrollViewView(), "ScrollView", b),
            new AakDocumentWellViewModel(new StatusBarView(), "StatusBar", b),
            new AakDocumentWellViewModel(new TabControlView(), "TabControl", b),
            new AakDocumentWellViewModel(new TextBoxView(), "TextBox", b),
            new AakDocumentWellViewModel(new ToolBarView(), "ToolBar", b),
            new AakDocumentWellViewModel(new ToolTipView(), "ToolTip", b),
            new AakDocumentWellViewModel(new TreeViewView(), "TreeView", b),
        };
        collections = new ObservableCollection<AakCollectionViewModel>
        {
            b
        };
        Title =LangResource.controls;
        View = new StyleSelectorView { DataContext = this };
    }
        
    internal new void ActiveDocument(AakDocumentWell view)
    {
        workSpaceViewModel.AddOrActiveDocument(view);
    }

    internal void CloseTab(AakDocumentWell view)
    {
        workSpaceViewModel.CloseDocument(view);
    }
}