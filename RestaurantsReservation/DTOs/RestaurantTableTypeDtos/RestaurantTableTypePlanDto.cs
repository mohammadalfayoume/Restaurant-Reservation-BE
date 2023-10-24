namespace RestaurantsReservation.DTOs.RestaurantTableTypeDtos;

public class RestaurantTableTypePlanDto
{
    public int Id { get; set; }
    public string TableType { get; set; }
    public bool IsDeleted { get; set; } = false;
}
