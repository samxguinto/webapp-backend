using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
public class UserDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; } // Plain text password for API; hashed on the server

    public string? Role { get; set; }

    public List<PostDto> Posts { get; set; } = new List<PostDto>();
}

}
