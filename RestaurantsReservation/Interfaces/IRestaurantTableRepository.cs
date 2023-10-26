using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces;

public interface IRestaurantTableRepository : IBaseRepository<RestaurantTable>
{
    Task<IEnumerable<RestaurantTable>> GetRestaurantTablesAsync(int id);
}