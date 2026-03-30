using System.ComponentModel.DataAnnotations;

namespace webbir.Models;

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime PublishedDate { get; set; } = DateTime.Now;

    public string Author { get; set; } = "Admin";

    public int? UserId { get; set; }
}