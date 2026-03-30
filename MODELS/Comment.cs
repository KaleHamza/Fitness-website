using System.ComponentModel.DataAnnotations;

namespace webbir.Models;

public class Comment
{
    public int Id { get; set; }

    public int BlogPostId { get; set; }

    [Required]
    public string Author { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int? UserId { get; set; }
}