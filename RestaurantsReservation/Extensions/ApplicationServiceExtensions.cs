using Microsoft.EntityFrameworkCore;
using RestaurantsReservation.Data;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Repositories;

namespace RestaurantsReservation.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        string? connString = config.GetConnectionString("Default");
        services.AddDbContext<DataBaseContext>(options =>
        {
            options.UseSqlServer(connString);
        });
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IRestaurantTableRepository, RestaurantTableRepository>();
        //services.AddScoped<IRestaurantTableTypeRepository, RestaurantTableTypeRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddCors();

        return services;
    }
}
