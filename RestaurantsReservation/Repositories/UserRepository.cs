using RestaurantsReservation.Data;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using RestaurantsReservation.DTOs.UserDtos;
using AutoMapper;

namespace RestaurantsReservation.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataBaseContext context, IMapper mapper) 
    {
        _context = context;
        _mapper = mapper;
    }
    private IQueryable<AppUserDto> GetDtoUsers()
    {
        return _context.Users.ProjectTo<AppUserDto>(_mapper.ConfigurationProvider).AsQueryable();
    }
    private IQueryable<AppUser> GetUsers()
    {
        return _context.Users.Include(ur=>ur.Reservations).AsQueryable();
    }
    public async Task<AppUserDto?> GetDtoUserByIdAsync(int id)
    {
        return await GetDtoUsers().FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
    }
    public async Task<AppUser?> GetByIdAsync(int id)
    {
        return await GetUsers().FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
    }

    public async Task<IEnumerable<AppUserDto>> GetDtoUsersAsync()
    {
        return await GetDtoUsers().AsNoTracking().Where(u => u.IsDeleted == false).ToListAsync();
    }

    public async Task UpdateAsync(AppUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public Task<IEnumerable<AppUser>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(AppUser user)
    {
        throw new NotImplementedException();
    }
} 
