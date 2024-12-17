using CommunityToolkit.Mvvm.Input;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public sealed partial class DataSourceViewModel : AakToolWell,IRefreshList
{
    private  DataSourceContext? _dataSourceContext =
        App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;

   

    public DataSourceViewModel(WorkSpaceViewModel workSpaceViewModel)
    {
        this.workSpaceViewModel = workSpaceViewModel;
        Title = "数据源";
        View = new DataSourceView { DataContext = this };
        Load();
    }

    public void Load()
    {
        _dataSourceContext = App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;
        // 假设 _dataSourceContext 是数据源上下文
        if (_dataSourceContext == null) return;
        var connectionsFromDb = _dataSourceContext.DataSources.ToList();
        var b = new BasicCollectionViewModel(this);

        var item = new ObservableCollection<AakDocumentWell>();

        var temp = connectionsFromDb.Select(c =>
            new AakDocumentWellViewModel(new DataSourceEditorView(new DataSourceEditorViewModel
            {
                Parent = this,
                DataSource = c,
            }), c.Name, b));
        foreach (var vm in temp)
        {
            item.Add(vm);
        }

        Collections = [b];
        b.Items = item;

        // Collections = new ObservableCollection<AakCollectionViewModel>(connectionsFromDb);
    }

    [RelayCommand]
    private void OnActive(object sender) => ActiveDocument(new AakDocumentWellViewModel(
        new DataSourceEditorView(new DataSourceEditorViewModel { Parent = this }),
        "DataSourceEditor", new BasicCollectionViewModel(this)));


    protected override void DeleteEntityByName()
    {
        if (SelectedItem is not AakDocumentWellViewModel vm || _dataSourceContext == null) return;
        var entity = _dataSourceContext.DataSources.FirstOrDefault(s => s.Name.Equals(vm.Title));
        if (entity != null) _dataSourceContext.DataSources.Remove(entity);
        _dataSourceContext.SaveChanges();
    }
}