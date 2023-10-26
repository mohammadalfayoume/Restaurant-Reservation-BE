using Microsoft.AspNetCore.Identity;
using RestaurantsReservation.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace RestaurantsReservation.Data;

public class Seed
{
    public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var users = await GetUsersFromJsonFileAsync();

        var roles = await GetRolesFromJsonFileAsync();

        await SeedRolesAsync(roles, roleManager);

        await SeedUsersAsync(users, userManager);

        await SeedAdminAsync(userManager);

    }

    private static async Task SeedAdminAsync(UserManager<AppUser> userManager)
    {
        var admin = new AppUser()
        {
            FirstName = "Mohammad",
            LastName = "Alfayoume",
            UserName = "admin",
            Email = "admin@admin.com",
            Created = DateTime.UtcNow
        };
        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRoleAsync(admin, "Admin");
    }

    private static async Task SeedUsersAsync(IEnumerable<AppUser> users, UserManager<AppUser> userManager)
    {
        foreach (var user in users)
        {
            user.UserName = user.UserName.ToLower();
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "User");
        }
    }

    private static async Task SeedRolesAsync(IEnumerable<AppRole> roles, RoleManager<AppRole> roleManager)
    {
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
    }

    private static async Task<IEnumerable<AppUser>> GetUsersFromJsonFileAsync()
    {
        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var users = JsonSerializer.Deserialize<IEnumerable<AppUser>>(userData, options);

        return users;
    }
    private static async Task<IEnumerable<AppRole>> GetRolesFromJsonFileAsync()
    {
        var userData = await File.ReadAllTextAsync("Data/RoleSeedData.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var roles = JsonSerializer.Deserialize<IEnumerable<AppRole>>(userData, options);

        return roles;
    }
}
