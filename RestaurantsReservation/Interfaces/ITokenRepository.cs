using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces;

public interface ITokenRepository
{
    Task<string> CreateToken(AppUser user);
}
