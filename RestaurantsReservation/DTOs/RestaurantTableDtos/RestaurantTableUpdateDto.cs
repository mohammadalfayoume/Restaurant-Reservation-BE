﻿namespace RestaurantsReservation.DTOs.RestaurantTableDtos;

public class RestaurantTableUpdateDto
{
    public int SeatingCapacity { get; set; }
    public int TableNumber { get; set; }
    public decimal Price { get; set; }
    public bool IsLoungeTable { get; set; }
    public string? Description { get; set; }
}
