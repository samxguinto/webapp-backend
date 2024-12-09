using System.Collections.Generic;

namespace WebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; } // For login
        public string? PasswordHash { get; set; } // Store hashed password
        public string Role { get; set; } = "User"; // Default role (User/Admin)
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
