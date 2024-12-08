namespace WebApp.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; } // Nullable to handle cases with no user
    }
}
