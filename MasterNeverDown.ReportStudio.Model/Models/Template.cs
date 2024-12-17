namespace CommunityToolkit.ReportEditor.Model.Models;

public class Template
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string? DataSource { get; set; }

    public int RowCount { get; set; } = 200;

    public int ColumnCount { get; set; } = 100;

    public string? CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}