using RestaurantsReservation.DTOs.UserDto;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces;

public interface IUserRepository
{
    Task UpdateAsync(AppUser user);
    Task<IEnumerable<AppUserDto>> GetDtoUsersAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUserDto?> GetDtoUserByIdAsync(int id);
}
