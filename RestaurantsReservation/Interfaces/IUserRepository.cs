using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces;

public interface IUserRepository
{
    Task UpdateAsync(AppUser user);
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
}
