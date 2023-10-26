using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestaurantsReservation.Data;
using RestaurantsReservation.Models;
using System.Text;

namespace RestaurantsReservation.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration congig)
    {
        // Specify the Identity behaviour
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            options.User.RequireUniqueEmail = true;

        }).AddTokenProvider<DataProtectorTokenProvider<AppUser>>("NZWalks").AddEntityFrameworkStores<DataBaseContext>().AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(congig["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                });
        // Add authorization policy
        //services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("AdminOnly", policy =>
        //        policy.RequireRole("Admin"));

        //    options.AddPolicy("RegularUserOnly", policy =>
        //        policy.RequireRole("User"));
        //});

        return services;
    }
}
