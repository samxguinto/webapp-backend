using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public List<PostDto> Posts { get; set; } = new List<PostDto>();
}

}
