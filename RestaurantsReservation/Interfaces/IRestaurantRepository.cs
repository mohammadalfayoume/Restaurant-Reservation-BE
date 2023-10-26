using RestaurantsReservation.DTOs;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces;

public interface IRestaurantRepository : IBaseRepository<Restaurant>
{
    Task<IEnumerable<Restaurant>> GetRestaurantByNameAsync(string name);
}
