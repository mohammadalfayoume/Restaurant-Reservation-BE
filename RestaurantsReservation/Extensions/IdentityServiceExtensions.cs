using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RestaurantsReservation.Data;
using RestaurantsReservation.Models;
using System.Text;

namespace RestaurantsReservation.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration congig)
    {
        var tokenKey = congig["TokenKey"];
        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signInKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        // Add authorization policy
        services.AddAuthorization();

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

        }).AddEntityFrameworkStores<DataBaseContext>();
        return services;
    }
}
