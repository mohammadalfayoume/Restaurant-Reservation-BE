namespace RestaurantsReservation.Models;

public class Restaurant : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string OpenAt { get; set; } = string.Empty;
    public string CloseAt { get; set; } = string.Empty;
    public double Rating { get; set; }
    public ICollection<RestaurantTable> Tables { get; set; } = new List<RestaurantTable>();
}
