using RestaurantsReservation.Data;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantsReservation.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _context;

    public UserRepository(DataBaseContext context) 
    {
        _context = context;
    }
    private IQueryable<AppUser> GetUsers()
    {
        return _context.Users.Include(ur=>ur.Reservations).AsQueryable();
    }
    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await GetUsers().FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await GetUsers().AsNoTracking().Where(u => u.IsDeleted == false).ToListAsync();
    }

    public async Task UpdateAsync(AppUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
} 
