using System.Collections.Generic;

namespace WebApp.Models
{
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "User"; // Default to "User"
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}

}
