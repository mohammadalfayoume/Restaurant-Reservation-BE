using RestaurantsReservation.Data;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantsReservation.Repositories;

public class RestaurantTableTypeRepository : IRestaurantTableTypeRepository
{
    private readonly DataBaseContext _context;
    public RestaurantTableTypeRepository(DataBaseContext context)
    {
        _context = context;
    }
    private IQueryable<RestaurantTableType> GetRestaurantTableTypes()
    {
        return _context.RestaurantTableTypes.Include(rtt=>rtt.RestaurantTable).AsQueryable();
    }
    public async Task CreateAsync(RestaurantTableType restaurantTableType)
    {
        _context.RestaurantTableTypes.Add(restaurantTableType);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RestaurantTableType>> GetAllAsync()
    {
        return await GetRestaurantTableTypes().AsNoTracking().Where(rt => rt.IsDeleted == false).ToListAsync();
    }

    public async Task<RestaurantTableType> GetByIdAsync(int id)
    {
        return await GetRestaurantTableTypes().FirstOrDefaultAsync(rtt => rtt.Id == id && rtt.IsDeleted==false);
    }

    public async Task UpdateAsync(RestaurantTableType restaurantTableType)
    {
        _context.RestaurantTableTypes.Update(restaurantTableType);
        await _context.SaveChangesAsync();
    }
}
