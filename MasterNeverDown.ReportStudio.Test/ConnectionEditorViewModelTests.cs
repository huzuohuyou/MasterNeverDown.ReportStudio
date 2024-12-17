using Moq;
using AakStudio.Shell.UI.Showcase.ViewModels;
using CommunityToolkit.ReportEditor.Model.Models;
using Microsoft.EntityFrameworkCore;

public class ConnectionEditorViewModelTests
{
    private readonly Mock<DataSourceContext> _mockDataSourceContext;
    private readonly ConnectionEditorViewModel _viewModel;

    public ConnectionEditorViewModelTests()
    {
        _mockDataSourceContext = new Mock<DataSourceContext>();
        _viewModel = new ConnectionEditorViewModel
        {
            Parent = new ConnectionViewModel(null)
        };
    }

    [Fact]
    public void SaveConnection_AddsNewConnection_WhenConnectionDoesNotExist()
    {
        var newConnection = new Connection { Id = 1, Name = "NewConnection" };
        _viewModel.Connection = newConnection;

        _mockDataSourceContext.Setup(x => x.Connections).Returns((DbSet<Connection>)new List<Connection>().AsQueryable());
        _viewModel.SaveConnection();

        _mockDataSourceContext.Verify(x => x.Connections.Add(It.Is<Connection>(c => c.Name == "NewConnection")), Times.Once);
        _mockDataSourceContext.Verify(x => x.SaveChanges(), Times.Once);
    }

    [Fact]
    public void SaveConnection_UpdatesExistingConnection_WhenConnectionExists()
    {
        var existingConnection = new Connection { Id = 1, Name = "ExistingConnection" };
        _mockDataSourceContext.Setup(x => x.Connections).Returns((DbSet<Connection>)new List<Connection> { existingConnection }.AsQueryable());

        _viewModel.Connection = new Connection { Id = 1, Name = "UpdatedConnection" };
        _viewModel.SaveConnection();

        Assert.Equal("UpdatedConnection", existingConnection.Name);
        _mockDataSourceContext.Verify(x => x.SaveChanges(), Times.Once);
    }

    [Fact]
    public void SaveConnection_DoesNotSave_WhenDataSourceContextIsNull()
    {
        var viewModel = new ConnectionEditorViewModel
        {
            Parent = new ConnectionViewModel(null),
            Connection = new Connection { Id = 1, Name = "NewConnection" }
        };

        viewModel.SaveConnection();

        _mockDataSourceContext.Verify(x => x.Connections.Add(It.IsAny<Connection>()), Times.Never);
        _mockDataSourceContext.Verify(x => x.SaveChanges(), Times.Never);
    }

    [Fact]
    public void OnSave_CallsSaveConnection()
    {
        var mockViewModel = new Mock<ConnectionEditorViewModel>();
        mockViewModel.Setup(x => x.SaveConnection());

        mockViewModel.Object.OnSave(null);

        mockViewModel.Verify(x => x.SaveConnection(), Times.Once);
    }
}