using RestaurantsReservation.DTOs.UserDto;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces;

public interface IUserRepository : IBaseRepository<AppUser>
{
    Task<IEnumerable<AppUserDto>> GetDtoUsersAsync();
    Task<AppUserDto?> GetDtoUserByIdAsync(int id);
}
