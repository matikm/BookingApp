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
    public class CustomerRepository : ICustomerRepository 
    {
        private readonly BookingAppContext _context;
        public CustomerRepository(BookingAppContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Customer>> GetCustomers()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            await _context.AddAsync(customer);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<Customer> GetCustomer(int? id)
        {
            if (id == null) return null;
            return await _context.Customer.FindAsync(id);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {         
            _context.Entry(_context.Customer.FirstOrDefault(c => c.Id == customer.Id)).CurrentValues.SetValues(customer);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteCustomer(int? id)
        {
            if (id != null)
            {
                var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);
                _context.Customer.Remove(customer);
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                else 
                    return false;
            }
            return false; 
        }
    }
}
