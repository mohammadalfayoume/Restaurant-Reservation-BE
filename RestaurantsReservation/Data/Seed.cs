using Microsoft.AspNetCore.Identity;
using RestaurantsReservation.Models;
using System.Data.Entity;

namespace RestaurantsReservation.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var users = new List<AppUser>()
        {
            new AppUser()
            {
                FirstName = "Alaa",
                LastName = "Ramadan",
                UserName = "alaa123",
                Email = "alaa@ramadan.com",
                Created = DateTime.UtcNow
            },
            new AppUser()
            {
                FirstName = "Ahmad",
                LastName = "Alfayoume",
                UserName = "ahmad123",
                Email = "ahmad@alfayoume.com",
                Created = DateTime.UtcNow
            },
            new AppUser()
            {
                FirstName = "Malek",
                LastName = "Ramadan",
                UserName = "malek123",
                Email = "malek@ramadan.com",
                Created = DateTime.UtcNow
            }
        };
        var roles = new List<AppRole>()
        {
            new AppRole()
            {
                Name = "Admin",
            },
            new AppRole()
            {
                Name = "User"
            }
        };
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
        foreach (var user in users)
        {
            user.UserName = user.UserName.ToLower();
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "User");
        }

        var admin = new AppUser()
        {
            FirstName = "Mohammad",
            LastName = "Alfayoume",
            UserName = "admin",
            Email = "admin@alfayoume.com",
            Created = DateTime.UtcNow
        };
        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRolesAsync(admin, new[] {"Admin", "User"});
    }
}
