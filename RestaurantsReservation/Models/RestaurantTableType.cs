namespace RestaurantsReservation.Models;

public class RestaurantTableType
{
    public int Id { get; set; }
    public string TableType { get; set; }
    public bool IsDeleted { get; set; } = false;
    public RestaurantTable RestaurantTable { get; set; }
}
