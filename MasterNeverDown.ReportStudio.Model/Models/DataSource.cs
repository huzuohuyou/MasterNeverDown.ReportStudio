namespace CommunityToolkit.ReportEditor.Model.Models;

public class DataSource
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string? Connection { get; set; }
    
    public string Script { get; set; }

    public string? LastResult { get; set; } = "123";
    
    
    public string CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}