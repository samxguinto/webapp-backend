using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;       
using WebApp.Models;     

public static class DbInitializer
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Ensure database is created
        context.Database.EnsureCreated();

        // Seed only if no users exist
        if (!context.Users.Any())
        {
            var User = new User
            {
                Name = "Sam",
                Email = "sam@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin",
                Posts = new List<Post>
                {
                    new Post { Title = "Hello Professor!", Content = "Have fun creating editing and deleting posts in the app. I recommend clicking the register button and registering with an email and password and then logging in. If you check the console you can see the JWT token displayed. Then feel free to create edit and delete users and posts. Remember posts must be tied to a valid user ID or they wont be created. Have fun! " }
                }
            };

            context.Users.Add(User);
            context.SaveChanges();
        }
    }
}
