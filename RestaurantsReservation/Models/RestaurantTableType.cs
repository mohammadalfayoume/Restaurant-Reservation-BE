namespace RestaurantsReservation.Models;

public class RestaurantTableType : BaseModel
{
    public string TableType { get; set; }
    public bool IsDeleted { get; set; } = false;
    public RestaurantTable RestaurantTable { get; set; }
}
