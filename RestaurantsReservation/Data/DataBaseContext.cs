using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Data;

public class DataBaseContext : IdentityDbContext
    <AppUser, AppRole, int,
    IdentityUserClaim<int>, IdentityUserRole<int>,
    IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DataBaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<RestaurantTable> RestaurantTables { get; set; }
    public DbSet<ReservationSchedule> Reservations { get; set; }

    [DbFunction(name:"SOUNDEX", IsBuiltIn =  true)]
    public string FuzzySearch(string query)
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(DataBaseContext).Assembly);
    }

}
