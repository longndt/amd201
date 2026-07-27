using System.ComponentModel.DataAnnotations;

namespace AuthService.Models;

// Represents a registered user stored in the Auth database.
public class AppUser
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    // We NEVER store the raw password. We store a salted hash instead.
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
