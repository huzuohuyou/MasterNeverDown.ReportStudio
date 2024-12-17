namespace CommunityToolkit.ReportEditor.Model.Models;

public class Parameter
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string? DisplayName { get; set; }
    
    public string? Connection { get; set; }
    
    public InputType? InputType { get; set; }
    
    public DataSourceType DataSourceType { get; set; }
    
    public string Script { get; set; }
    
    public string CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}