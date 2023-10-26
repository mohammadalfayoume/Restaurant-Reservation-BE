namespace RestaurantsReservation.Models;

public class ReservationSchedule : BaseModel
{
    public string ReservationDate { get; set; }
    public string StartAt { get; set; }
    public string EndAt { get; set; }
    public bool IsReserved { get; set; } = false;
    public bool IsCanceled { get; set; } = false;
    public AppUser? User { get; set; }
    public RestaurantTable? RestaurantTable { get; set; }
}
