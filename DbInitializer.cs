using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;       // Namespace for ApplicationDbContext
using WebApp.Models;     // Namespace for User and Post

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
            var adminUser = new User
            {
                Name = "Admin",
                Email = "admin@example.com", // Required Email
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), // Secure password
                Role = "Admin",
                Posts = new List<Post>
                {
                    new Post { Title = "Welcome Post", Content = "This is the first post by Admin!" }
                }
            };

            context.Users.Add(adminUser);
            context.SaveChanges();
        }
    }
}
