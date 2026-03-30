using System.ComponentModel.DataAnnotations;

namespace webbir.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public double? HeightCm { get; set; }
    public double? WeightKg { get; set; }
    public string ProgramName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;

    public double? BMI => (HeightCm.HasValue && WeightKg.HasValue && HeightCm.Value > 0)
        ? Math.Round(WeightKg.Value / Math.Pow(HeightCm.Value / 100.0, 2), 1)
        : null;
}