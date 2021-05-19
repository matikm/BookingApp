using BookingApp.Data;
using BookingApp.Interfaces;
using BookingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly BookingAppContext _context;

        public ReservationRepository(BookingAppContext context)
        {
            _context = context;
        }

        public async Task<bool> AddReservation(Reservation reservation)
        {
            await _context.AddAsync(reservation);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteReservation(int? id)
        {
            if (id != null)
            {
                var reservation = await _context.Reservation.FirstOrDefaultAsync(c => c.Id == id);
                _context.Reservation.Remove(reservation);
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<Reservation> GetReservation(int? id)
        {
            if (id == null) return null;
            return await _context.Reservation.FindAsync(id);
        }

        public async Task<ICollection<Reservation>> GetReservations()
        {
            return await _context.Reservation.OrderBy(d => d.CheckIn).ToListAsync();
        }

        public async Task<ICollection<Reservation>> GetReservations(DateTime fromDate)
        {
            return await _context.Reservation.Where(d => d.CheckOut >= fromDate).OrderBy(d => d.CheckIn).ToListAsync();
        }
        public async Task<ICollection<Reservation>> GetReservations(DateTime fromDate, DateTime untilDate)
        {
            return await _context.Reservation.Where(d => d.CheckOut >= fromDate && d.CheckIn <= untilDate).OrderBy(d => d.CheckIn).ToListAsync();
        }

        public async Task<bool> UpdateReservation(Reservation reservation)
        {
            _context.Entry(await _context.Reservation.FirstOrDefaultAsync(c => c.Id == reservation.Id)).CurrentValues.SetValues(reservation);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
