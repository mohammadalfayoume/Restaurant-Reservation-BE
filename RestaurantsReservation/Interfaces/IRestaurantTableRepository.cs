using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces;

public interface IRestaurantTableRepository
{
    Task UpdateAsync(RestaurantTable restaurantTable);
    Task<IEnumerable<RestaurantTable>> GetTablesAsync();
    Task<RestaurantTable?> GetByIdAsync(int id);
    Task<IEnumerable<RestaurantTable>> GetRestaurantTablesAsync(int id);
    Task CreateAsync(RestaurantTable restaurantTable);
}