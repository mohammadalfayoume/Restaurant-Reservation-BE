using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces;

public interface IRestaurantTableTypeRepository
{
    Task UpdateAsync(RestaurantTableType restaurantTableType);
    Task<IEnumerable<RestaurantTableType>> GetAllAsync();
    Task<RestaurantTableType?> GetByIdAsync(int id);
    Task CreateAsync(RestaurantTableType restaurantTableType);
}
