using RestaurantsReservation.Models;

namespace RestaurantsReservation.Interfaces
{
    public interface IReservationRepository : IBaseRepository<ReservationSchedule>
    {
        Task<IEnumerable<ReservationSchedule>> GetUserReservationsAsync(int userId);
        Task<IEnumerable<ReservationSchedule>> GetTableReservationsAsync(int tableId);
    }
}
