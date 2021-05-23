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
    public class ObjectForRentRepository : IObjectForRentRepository
    {
        private readonly BookingAppContext _context;

        public ObjectForRentRepository(BookingAppContext context)
        {
            _context = context;
        }
        public async Task<bool> AddObjectForRent(ObjectForRent objectForRent)
        {
            await _context.AddAsync(objectForRent);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteObjectForRent(int? id)
        {
            if (id != null)
            {
                var objectForRent = await _context.ObjectForRent.FirstOrDefaultAsync(c => c.ObjectForRentId == id);
                _context.ObjectForRent.Remove(objectForRent);
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<ObjectForRent> GetObjectForRent(int? id)
        {
            if (id == null) return null;
            return await _context.ObjectForRent.FindAsync(id);
        }

        public async Task<ICollection<ObjectForRent>> GetObjectForRents()
        {
            return await _context.ObjectForRent.ToListAsync();
        }

        public async Task<bool> UpdateObjectForRent(ObjectForRent objectForRent)
        {
            _context.Entry(_context.ObjectForRent.FirstOrDefault(c => c.ObjectForRentId == objectForRent.ObjectForRentId)).CurrentValues.SetValues(objectForRent);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
