using Microsoft.EntityFrameworkCore;

namespace CommunityToolkit.ReportEditor.Model.Models;

public class DataSourceContext: DbContext
{
    public DbSet<Template> Templates { get; set; }
    
    public DbSet<DataSource> DataSources { get; set; }
    
    public DbSet<Parameter> Parameters { get; set; }
    
    public DbSet<Connection> Connections { get; set; }

    private string DbPath { get; }
    public static DataSourceContext Factory { get; } = new();

    public DataSourceContext()
    {
        DbPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "KnockKnock.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}