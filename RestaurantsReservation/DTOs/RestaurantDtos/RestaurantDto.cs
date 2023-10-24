using RestaurantsReservation.Models;
using RestaurantsReservation.DTOs.RestaurantTableDtos;

namespace RestaurantsReservation.DTOs.RestaurantDtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string? OpenAt { get; set; }
    public string? CloseAt { get; set; }
    public double Rating { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public ICollection<RestaurnatTablePlanDto> Tables { get; set; } = new List<RestaurnatTablePlanDto>();
}
