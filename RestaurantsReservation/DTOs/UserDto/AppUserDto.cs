using RestaurantsReservation.DTOs.ReservationDtos;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.DTOs.UserDto;

public class AppUserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }
    public ICollection<ReservationPlanDto> Reservations { get; set; } = new List<ReservationPlanDto>();
}
