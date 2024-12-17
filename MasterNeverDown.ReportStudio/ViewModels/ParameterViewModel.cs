using CommunityToolkit.Mvvm.Input;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public sealed partial class ParameterViewModel : AakToolWell, IRefreshList
{
    private DataSourceContext? _dataSourceContext =
        App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;

    protected override void DeleteEntityByName()
    {
        if (_dataSourceContext == null) return;
        var entity = _dataSourceContext.Parameters.FirstOrDefault(s => s.Name.Equals(Name));
        if (entity != null) _dataSourceContext.Parameters.Remove(entity);
        _dataSourceContext.SaveChanges();
    }

    public ParameterViewModel(WorkSpaceViewModel workSpaceViewModel)
    {
        this.workSpaceViewModel = workSpaceViewModel;
        Title = "参数";
        View = new DataSourceView { DataContext = this };
        Load();
    }

    public void Load()
    {
        _dataSourceContext = App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;
        // 假设 _dataSourceContext 是数据源上下文
        if (_dataSourceContext == null) return;
        var connectionsFromDb = _dataSourceContext.Parameters.ToList();
        var b = new BasicCollectionViewModel(this);

        var item = new ObservableCollection<AakDocumentWell>();

        var temp = connectionsFromDb.Select(c =>
            new AakDocumentWellViewModel(new ParameterEditorView(new ParameterEditorViewModel
            {
                Parent = this,
                Parameter = c,
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
        new ParameterEditorView(new ParameterEditorViewModel { Parent = this }),
        "ParameterEditor", new BasicCollectionViewModel(this)));
}