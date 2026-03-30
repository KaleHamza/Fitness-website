using System.ComponentModel.DataAnnotations;

namespace webbir.Models;

public class Exercise
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
}