using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Data.Config
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(r => r.Name).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(r => r.Description).HasColumnType("nvarchar(250)");
            builder.Property(r => r.Region).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(r => r.Country).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(r => r.City).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(r => r.OpenAt).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(r => r.CloseAt).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(r => r.Region).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(r => r.Rating).IsRequired();
            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
