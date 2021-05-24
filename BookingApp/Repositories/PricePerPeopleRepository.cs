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
    public class PricePerPeopleRepository : IPricePerPeopleRepository
    {
        private readonly BookingAppContext _context;
        public PricePerPeopleRepository(BookingAppContext context)
        {
            _context = context;
        }
        public async Task<bool> AddPricePerPeople(PricePerPeople pricePerPeople)
        {
            await _context.AddAsync(pricePerPeople);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> DeletePricePerPeople(int? id)
        {
            if (id != null)
            {
                var pricePerPeople = await _context.PricePerPeople.FirstOrDefaultAsync(c => c.PricePerPeopleId == id);
                _context.PricePerPeople.Remove(pricePerPeople);
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<PricePerPeople> GetPricePerPeople(int? id)
        {
            if (id == null) return null;
            return await _context.PricePerPeople.FindAsync(id);
        }

        public async Task<ICollection<PricePerPeople>> GetPriceListForObject(int? id)
        {
            if (id == null) return null;
            return await _context.PricePerPeople.Where(o => o.ObjectForRent.ObjectForRentId == id).OrderBy(x=>x.People).ToListAsync();
        }

        public async Task<ICollection<PricePerPeople>> GetPricePerPeoples()
        {
            return await _context.PricePerPeople.ToListAsync();
        }

        public async Task<bool> UpdatePricePerPeople(PricePerPeople pricePerPeople)
        {
            _context.Entry(await _context.PricePerPeople.FirstOrDefaultAsync(c => c.PricePerPeopleId == pricePerPeople.PricePerPeopleId)).CurrentValues.SetValues(pricePerPeople);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
