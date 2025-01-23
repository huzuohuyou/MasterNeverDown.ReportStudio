using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public partial class DataSourceEditorViewModel : AakToolWell
{
    private readonly DataSourceContext? _dataSourceContext =
        App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;
    public DataSourceViewModel Parent { get; set; } = null!;
    private DataSource _dataSource = new();

    public DataSource DataSource
    {
        get => _dataSource;
        set => SetProperty(ref _dataSource, value);
    }
    public ObservableCollection<string> Connections { get; set; } = new();

    [ObservableProperty]private string _inputString = null!;

    [ObservableProperty] private string lastResult = null!;
   
    public DataSourceEditorViewModel()
    {
        LoadConnections();
    }
    
    private void LoadConnections()
    {
        if (_dataSourceContext == null) return;
        var connectionsFromDb = _dataSourceContext.Connections.Select(s=>s.Name).ToList();
        Connections = new ObservableCollection<string>(connectionsFromDb);
    }

    [RelayCommand]
    private void OnSave(object sender) => SaveDataSource();

   

    private void SaveDataSource()
    {
        if (_dataSourceContext != null)
        {
            var entity = _dataSourceContext.DataSources.FirstOrDefault(ds => ds.Id == DataSource.Id);
            if (entity != null)
            {
                // Update existing data source
                entity.Name = DataSource.Name;
                entity.Connection = DataSource.Connection;
                entity.Script = InputString;
                InputString = entity.Script;
            }
            else
            {
                // Add new data source
                DataSource.Script = InputString;
                _dataSourceContext.DataSources.Add(DataSource);
                InputString =  DataSource.Script;
            }

            
            _dataSourceContext.SaveChanges();
            DataSource = _dataSourceContext.DataSources.FirstOrDefault(ds => ds.Name.Equals(DataSource.Name))!;
        }
        Parent.Load();
    }
    
    [RelayCommand]
    private void OnExecute(object sender) => Execute();
    
    private void Execute()
    {
        if (_dataSourceContext != null)
        {
            var conn = _dataSourceContext.Connections.FirstOrDefault(c => c.Name.Equals(DataSource.Connection));

            if (conn != null)
            {
                var connectionString = GetConnectionString(conn.Host, conn.User, conn.Password, conn.Database, conn.Port.ToString());
                using IDbConnection db = new NpgsqlConnection(connectionString);
                var result = db.Query<dynamic>(DataSource.Script).ToList();
                LastResult = JsonConvert.SerializeObject(result.Take(100));
                DataSource.LastResult = LastResult;
            }
        }
    }

    private string GetConnectionString(string servername, string uid, string pwd, string db, string port)
    {
        return $"host={servername};User ID={uid};password={pwd};database={db};port={port};pooling=false;";
    }

    protected override void DeleteEntityByName()
    {
        throw new NotImplementedException();
    }
}