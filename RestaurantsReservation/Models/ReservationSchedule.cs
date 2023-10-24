﻿namespace RestaurantsReservation.Models;

public class ReservationSchedule
{
    public int Id { get; set; }
    public string ReservationDate { get; set; }
    public string StartAt { get; set; }
    public string EndAt { get; set; }
    public bool IsReserved { get; set; } = false;
    public bool IsCanceled { get; set; } = false;
    public AppUser? User { get; set; }
    public RestaurantTable? RestaurantTable { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
