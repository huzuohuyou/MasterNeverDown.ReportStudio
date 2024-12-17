using CommunityToolkit.Mvvm.Input;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public sealed partial class ConnectionViewModel : AakToolWell,IRefreshList
{
   

    public ConnectionViewModel(WorkSpaceViewModel workSpaceViewModel)
    {
        this.workSpaceViewModel = workSpaceViewModel;
        Title = "连接";

        View = new ConnectionView { DataContext = this };
        Load();
    }

    public void Load()
    {
        using var dataSourceContext = new DataSourceContext();
        var connectionsFromDb = dataSourceContext.Connections.ToList();
        var b = new BasicCollectionViewModel(this);

        var item = new ObservableCollection<AakDocumentWell>();

        var temp = connectionsFromDb.Select(c =>
            new AakDocumentWellViewModel(new ConnectionEditorView(new ConnectionEditorViewModel
            {
                Connection = c,
                Parent = this
            }), c.Name, b));
        foreach (var vm in temp)
        {
            item.Add(vm);
        }

        Collections = [b];
        b.Items = item;
    }

    [RelayCommand]
    private void OnActive(object sender) => ActiveDocument(new AakDocumentWellViewModel(
        new ConnectionEditorView(new ConnectionEditorViewModel{Parent = this} ),
        "ConnectionEditor", new BasicCollectionViewModel(this)));


    protected override void DeleteEntityByName()
    {
        using var dataSourceContext = new DataSourceContext();
        var entity = dataSourceContext.Connections.FirstOrDefault(s => s.Name.Equals(Name));
        if (entity != null) dataSourceContext.Connections.Remove(entity);
        dataSourceContext.SaveChanges();
    }
}