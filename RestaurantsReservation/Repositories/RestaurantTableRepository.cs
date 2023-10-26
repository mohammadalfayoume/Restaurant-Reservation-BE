using RestaurantsReservation.Data;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantsReservation.Repositories;

public class RestaurantTableRepository : IRestaurantTableRepository
{
    private readonly DataBaseContext _context;

    public RestaurantTableRepository(DataBaseContext context)
    {
        _context = context;
    }
    private IQueryable<RestaurantTable> GetRestaurantTables()
    {
        return _context.RestaurantTables.Include(rt=>rt.RestaurantTableType).Include(rt=>rt.Reservations).Include(rt=>rt.Restaurant).AsQueryable();
    }
    public async Task CreateAsync(RestaurantTable restaurantTable)
    {
        _context.RestaurantTables.Add(restaurantTable);
        await _context.SaveChangesAsync();
    }

    public async Task<RestaurantTable> GetByIdAsync(int id)
    {
        return await GetRestaurantTables().FirstOrDefaultAsync(rt => rt.Id == id && rt.IsDeleted == false);
    }

    public async Task<IEnumerable<RestaurantTable>> GetAllAsync()
    {
        return await GetRestaurantTables().AsNoTracking().Where(res => res.IsDeleted == false).ToListAsync();
    }

    public async Task UpdateAsync(RestaurantTable restaurantTable)
    {
        _context.RestaurantTables.Update(restaurantTable);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RestaurantTable>> GetRestaurantTablesAsync(int id)
    {
        return await GetRestaurantTables().AsNoTracking().Where(rt=>rt.IsDeleted==false && rt.Restaurant != null && rt.Restaurant.Id == id && rt.Restaurant.IsDeleted==false).ToListAsync();
    }
}
