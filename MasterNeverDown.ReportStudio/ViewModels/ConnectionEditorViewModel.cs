using CommunityToolkit.Mvvm.Input;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public partial class ConnectionEditorViewModel : AakToolWell
{
    public ConnectionViewModel Parent { get; set; } = null!;
    [ObservableProperty] private Connection _connection = new();
    [ObservableProperty] private bool _isSaveEnabled;

    public void SaveConnection()
    {
        using var dataSourceContext = new DataSourceContext();
        var entity = dataSourceContext.Connections.FirstOrDefault(c => c.Id == Connection.Id);
        if (entity != null)
        {
            // Update existing connection
            entity.Name = Connection.Name;
            entity.Host = Connection.Host;
            entity.Port = Connection.Port;
            entity.User = Connection.User;
            entity.Password = Connection.Password;
            entity.Database = Connection.Database;
        }
        else
        {
            // Add new connection
            dataSourceContext.Connections.Add(Connection);
        }

        dataSourceContext.SaveChanges();
        Connection = dataSourceContext.Connections.FirstOrDefault(c => c.Name.Equals(Connection.Name))!;
        Parent.Load();
    }

    [RelayCommand]
    public void OnSave(object sender) => SaveConnection();

    [RelayCommand]
    private void OnTest(object sender) => TestConnection();

    private void TestConnection()
    {
        using IDbConnection db = new NpgsqlConnection(Connection.PostgreSQLConnectionString);
        db.Open();
        MessageBox.Show("Connection successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    protected override void DeleteEntityByName()
    {
        throw new NotImplementedException();
    }
}