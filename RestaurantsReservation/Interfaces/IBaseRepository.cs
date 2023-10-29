
namespace RestaurantsReservation.Interfaces;
public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task CreateAsync(T reservation);
    Task UpdateAsync(T reservation);
}
