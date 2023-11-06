namespace RestaurantsReservation.Models;

public class RestaurantTable : BaseModel
{
    public RestaurantTableTypeEnums RestaurantTableType { get; set; }
    public int SeatingCapacity { get; set; }
    public int TableNumber { get; set; }
    public decimal Price { get; set; }
    public bool IsLoungeTable { get; set; }
    public string Description { get; set; } = string.Empty;
    public ICollection<ReservationSchedule> Reservations { get; set; }= new List<ReservationSchedule>();
    public int? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}

public enum RestaurantTableTypeEnums
{
    Indoor,
    Outdoor
}