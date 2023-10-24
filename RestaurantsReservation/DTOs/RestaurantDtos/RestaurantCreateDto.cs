namespace RestaurantsReservation.DTOs.RestaurantDtos;

public class RestaurantCreateDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public string OpenAt { get; set; }
    public string CloseAt { get; set; }
    public double Rating { get; set; }
}
