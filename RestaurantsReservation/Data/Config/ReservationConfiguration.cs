using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Data.Config
{
    public class ReservationConfiguration : IEntityTypeConfiguration<ReservationSchedule>
    {
        public void Configure(EntityTypeBuilder<ReservationSchedule> builder)
        {
            builder.Property(r => r.ReservationDate).IsRequired();

            builder.HasOne(r => r.RestaurantTable)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.RestaurantTableId)
                .IsRequired(false);

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .IsRequired(false);
        }
    }
}
