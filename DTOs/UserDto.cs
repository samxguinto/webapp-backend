using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public List<PostDto> Posts { get; set; } = new List<PostDto>();
    }
}
