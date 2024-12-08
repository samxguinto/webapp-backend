namespace WebApp.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PostDto> Posts { get; set; }
    }
}
