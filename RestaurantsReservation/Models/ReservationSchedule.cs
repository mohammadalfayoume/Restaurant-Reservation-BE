﻿namespace RestaurantsReservation.Models;

public class ReservationSchedule : BaseModel
{
    public string ReservationDate { get; set; } = string.Empty;
    public string StartAt { get; set; } = string.Empty;
    public string EndAt { get; set; } = string.Empty;
    public bool IsReserved { get; set; } = false;
    public bool IsCanceled { get; set; } = false;
    public AppUser? User { get; set; }
    public RestaurantTable? RestaurantTable { get; set; }
}
