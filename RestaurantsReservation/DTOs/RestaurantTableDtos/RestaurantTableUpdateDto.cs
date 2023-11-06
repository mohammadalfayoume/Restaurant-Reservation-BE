namespace RestaurantsReservation.DTOs.RestaurantTableDtos;

public class RestaurantTableUpdateDto
{
    public string? RestaurantTableType { get; set; }
    public int SeatingCapacity { get; set; }
    public int TableNumber { get; set; }
    public decimal Price { get; set; }
    public bool IsLoungeTable { get; set; }
    public string? Description { get; set; }
}
