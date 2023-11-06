using RestaurantsReservation.DTOs.ReservationDtos;
using RestaurantsReservation.DTOs.RestaurantDtos;

namespace RestaurantsReservation.DTOs.RestaurantTableDtos;

public class RestaurantTableDto
{
    public int Id { get; set; }
    public string? RestaurantTableType { get; set; }
    public int SeatingCapacity { get; set; }
    public int TableNumber { get; set; }
    public decimal Price { get; set; }
    public bool IsLoungeTable { get; set; }
    public string? Description { get; set; }
    public ICollection<ReservationPlanDto> Reservations { get; set; } = new List<ReservationPlanDto>();
    public RestaurantPlanDto? Restaurant { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }
    public bool IsDeleted { get; set; } = false;
}
