using AakStudio.Shell.UI.Showcase.Models;
using CommunityToolkit.Mvvm.Input;
using MasterNeverDown.ReportStudio.Model.LangRes;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public partial class ParameterEditorViewModel : AakToolWell
{
    private readonly DataSourceContext? _dataSourceContext =
        App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;
    public ParameterViewModel Parent { get; set; } = null!;

    private Parameter _parameter = new();

    public Parameter Parameter
    {
        get => _parameter;
        set => SetProperty(ref _parameter, value);
    }

    public ObservableCollection<string> Connections { get; set; } = new();
    
    [ObservableProperty]private string _inputString = null!;
 

    private int _selectedTabIndex=2;
    public int SelectedTabIndex
    {
        get => (int)Parameter.DataSourceType;
        set => SetProperty(ref _selectedTabIndex, value);
    }
    
    public ParameterEditorViewModel()
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
        if (_dataSourceContext == null) return;
        var entity = _dataSourceContext.Parameters.FirstOrDefault(ds => ds.Name == Parameter.Name);
        if (entity != null)
        {
            // Update existing data source
            entity.Name = Parameter.Name;
            entity.DisplayName = Parameter.DisplayName;
            entity.Connection = Parameter.Connection;
            entity.InputType = Parameter.InputType;
            entity.DataSourceType = Parameter.DataSourceType;
            entity.Script = InputString;
        }
        else
        {
            // Add new data source
            Parameter.Script = InputString;
            _dataSourceContext.Parameters.Add(Parameter);
        }

        _dataSourceContext.SaveChanges();
        Parameter = _dataSourceContext.Parameters.FirstOrDefault(ds => ds.Name.Equals(Parameter.Name))!;
        InputString = Parameter.Script;
        Parent.Load();
    }
    
    [RelayCommand]
    private void OnExecute(object sender) => Execute();


    [ObservableProperty] private ObservableCollection<ComboxDataSource> _dataGridItems = null!;

    private void Execute()
    {
        try
        {
            if (_dataSourceContext == null) return;
            var conn = _dataSourceContext.Connections.FirstOrDefault(c => c.Name.Equals(Parameter.Connection));

            if (conn == null) 
                return;
            var connectionString =
                GetConnectionString(conn.Host, conn.User, conn.Password, conn.Database, conn.Port.ToString());
            using IDbConnection db = new NpgsqlConnection(connectionString);
            var result = db.Query<ComboxDataSource>(Parameter.Script ?? throw new InvalidOperationException()).ToList();
            DataGridItems = new ObservableCollection<ComboxDataSource>(result);
        }
        catch (TimeoutException)
        {
            MessageBox.Show(LangResource.Time_out);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
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