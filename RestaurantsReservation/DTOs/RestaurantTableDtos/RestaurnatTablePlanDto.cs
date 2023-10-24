using RestaurantsReservation.DTOs.RestaurantDtos;
using RestaurantsReservation.DTOs.RestaurantTableTypeDtos;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.DTOs.RestaurantTableDtos;

public class RestaurnatTablePlanDto
{
    public int Id { get; set; }
    public int SeatingCapacity { get; set; }
    public int TableNumber { get; set; }
    public decimal Price { get; set; }
    public bool IsLoungeTable { get; set; }
    public string? Description { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }
    public bool IsDeleted { get; set; } = false;
}
