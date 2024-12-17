using System.ComponentModel.DataAnnotations;

namespace CommunityToolkit.ReportEditor.Model.Models;

public class Connection
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    public string? Comment { get; set; }
    
    public string Host { get; set; }
    
    public string User { get; set; }

    public int Port { get; set; }
    
    public string Password { get; set; }
    
    public string Database { get; set; }

    public string CreateDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    
}