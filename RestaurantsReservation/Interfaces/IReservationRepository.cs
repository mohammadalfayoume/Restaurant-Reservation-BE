using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces
{
    public interface IReservationRepository
    {
        Task UpdateAsync(ReservationSchedule reservation);
        Task<IEnumerable<ReservationSchedule>> GetAllAsync();
        Task<ReservationSchedule?> GetByIdAsync(int id);
        Task CreateAsync(ReservationSchedule reservation);
        Task<IEnumerable<ReservationSchedule>> GetUserReservationsAsync(int userId);
        Task<IEnumerable<ReservationSchedule>> GetTableReservationsAsync(int tableId);
    }
}
