using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.DTOs;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
[HttpGet]
public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
{
    var users = await _context.Users
        .Include(u => u.Posts)
        .Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Posts = u.Posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                UserId = p.UserId,
                Name = u.Name // Map the user's name here
            }).ToList()
        })
        .ToListAsync();

    return Ok(users);
}


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Posts)
                .Where(u => u.Id == id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Posts = u.Posts.Select(p => new PostDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Content = p.Content
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        
        //post user
       [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Name))
                {
            return BadRequest(new
                {
                    Title = "Validation Error",
                    Errors = new { Name = "The Name field is required." }
                });
                 }

             var user = new Models.User
            {
            Name = userDto.Name,
            Posts = userDto.Posts?.Select(p => new Models.Post
                {
               Title = p.Title,
                Content = p.Content
                }).ToList() ?? new List<Models.Post>() // Handle null Posts
                };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    userDto.Id = user.Id;
    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
}


        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = userDto.Name;

            // Update Posts (simplified logic for demo purposes)
            user.Posts = userDto.Posts.Select(p => new Models.Post
            {
                Title = p.Title,
                Content = p.Content
            }).ToList();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
