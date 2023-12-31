﻿using Microsoft.EntityFrameworkCore;
using RestaurantsReservation.Data;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataBaseContext _context;

        public ReservationRepository(DataBaseContext context)
        {
            _context = context;
        }
        private IQueryable<ReservationSchedule> GetReservations()
        {
            return _context.Reservations.Include(rs=>rs.User).Include(rs=>rs.RestaurantTable).AsQueryable();
        }
        public async Task CreateAsync(ReservationSchedule reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReservationSchedule>> GetAllAsync()
        {
            return await GetReservations().AsNoTracking().ToListAsync();
        }

        public async Task<ReservationSchedule> GetByIdAsync(int id)
        {
            return await GetReservations().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(ReservationSchedule reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReservationSchedule>> GetUserReservationsAsync(int userId)
        {
            return await GetReservations().AsNoTracking().Where(res=> res.User != null && res.User.Id==userId).ToListAsync();
        }

        public async Task<IEnumerable<ReservationSchedule>> GetTableReservationsAsync(int tableId)
        {
            return await GetReservations().AsNoTracking().Where(res => res.RestaurantTable != null && res.RestaurantTable.Id == tableId).ToListAsync();
        }
    }
}
