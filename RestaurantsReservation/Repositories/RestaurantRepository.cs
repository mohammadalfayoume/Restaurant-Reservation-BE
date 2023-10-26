using Microsoft.EntityFrameworkCore;
using RestaurantsReservation.Data;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly DataBaseContext _context;

    public RestaurantRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();
    }
    private IQueryable<Restaurant> GetRestaurants()
    {
        return _context.Restaurants.Include(r=>r.Tables).AsQueryable();
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await GetRestaurants().FirstOrDefaultAsync(r=>r.Id==id && r.IsDeleted==false);
    }
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await GetRestaurants().AsNoTracking().Where(res=>res.IsDeleted==false).ToListAsync(); 
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurantByNameAsync(string name)
    {
        return await GetRestaurants().AsNoTracking().Where(e => _context.FuzzySearch(e.Name) == _context.FuzzySearch(name)).ToListAsync();
    }

}
