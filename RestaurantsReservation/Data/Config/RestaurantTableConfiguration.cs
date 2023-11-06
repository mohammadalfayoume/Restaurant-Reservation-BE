using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Data.Config
{
    public class RestaurantTableConfiguration : IEntityTypeConfiguration<RestaurantTable>
    {
        public void Configure(EntityTypeBuilder<RestaurantTable> builder)
        {
            builder.ToTable("RestaurantTable");
            builder.Property(t => t.SeatingCapacity).IsRequired();
            builder.Property(t => t.TableNumber).IsRequired();
            builder.Property(t => t.Price).HasColumnType("decimal(7,2)").IsRequired();
            builder.Property(t => t.Description).HasColumnType("nvarchar(250)");
            builder.Property(t => t.RestaurantTableType)
                .HasConversion(
                    t => t.ToString(),
                    t => (RestaurantTableTypeEnums)Enum.Parse(typeof(RestaurantTableTypeEnums), t));

            builder.HasOne(t => t.Restaurant)
                .WithMany(r => r.Tables)
                .HasForeignKey(t => t.RestaurantId)
                .IsRequired(false);
        }
    }
}
