using RestaurantsReservation.DTOs;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces;

public interface IRestaurantRepository
{
    Task UpdateAsync(Restaurant restaurant);
    Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int id);
    Task<IEnumerable<Restaurant>> GetRestaurantByNameAsync(string name);
    Task CreateAsync(Restaurant restaurant);
}
