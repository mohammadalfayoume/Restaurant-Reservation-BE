using RestaurantsReservation.DTOs.RestaurantDtos;
using RestaurantsReservation.DTOs.RestaurantTableDtos;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.DTOs.RestaurantTableTypeDtos;

public class RestaurantTableTypeDto
{
    public int Id { get; set; }
    public string TableType { get; set; }
    public bool IsDeleted { get; set; } = false;
    public RestaurnatTablePlanDto? RestaurantTable { get; set; }
}
