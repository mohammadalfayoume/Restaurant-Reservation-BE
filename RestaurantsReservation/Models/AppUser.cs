
using Microsoft.AspNetCore.Identity;

namespace RestaurantsReservation.Models;

public class AppUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }
    public bool IsDeleted { get; set; } = false;
    public ICollection<ReservationSchedule> Reservations { get; set; } = new List<ReservationSchedule>();
}
