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
    public DbSet<RestaurantTableType> RestaurantTableTypes { get; set; }
    public DbSet<ReservationSchedule> Reservations { get; set; }

    [DbFunction(name:"SOUNDEX", IsBuiltIn =  true)]
    public string FuzzySearch(string query)
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // SeedRoles(builder);

        var restaurants = builder.Entity<Restaurant>();
        restaurants.Property(r => r.Name).HasColumnType("nvarchar(20)").IsRequired();
        restaurants.Property(r => r.Description).HasColumnType("nvarchar(250)");
        restaurants.Property(r => r.Region).HasColumnType("nvarchar(50)").IsRequired();
        restaurants.HasMany(r => r.Tables).WithOne(t => t.Restaurant);
        //restaurants.HasMany(r => r.Reservations).WithOne(reservation => reservation.Restaurant);
        
        var tables = builder.Entity<RestaurantTable>();
        tables.ToTable("RestaurantTable");
        tables.Property(t => t.SeatingCapacity).IsRequired();
        tables.Property(t => t.TableNumber).IsRequired();
        tables.Property(t => t.Price).HasColumnType("decimal(7,2)").IsRequired();
        tables.Property(t => t.Description).HasColumnType("nvarchar(250)");
        tables.HasMany(t => t.Reservations).WithOne(reservation => reservation.RestaurantTable);

        var tableTyps = builder.Entity<RestaurantTableType>();
        tableTyps.HasOne(ty => ty.RestaurantTable).WithOne(t=>t.RestaurantTableType).HasForeignKey<RestaurantTable>("TableTypeId");

        var reservations = builder.Entity<ReservationSchedule>();
        reservations.Property(r => r.ReservationDate).IsRequired();

        var users = builder.Entity<AppUser>();
        users.HasMany(ur=> ur.Reservations).WithOne(res=>res.User);

    }

}
