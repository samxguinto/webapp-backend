using System.Collections.Generic;

namespace WebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
