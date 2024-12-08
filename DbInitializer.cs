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

        if (!context.Users.Any())
        {
            var user = new User
            {
                Name = "John Doe",
                Posts = new List<Post>
                {
                    new Post { Title = "First Post", Content = "Hello, world!" }
                }
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
