using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Returns customers from database.
        /// </summary>
        /// <returns>List of customers</returns>
        Task<ICollection<Customer>> GetCustomers();
        Task<bool> AddCustomer(Customer customer);
        Task<Customer> GetCustomer(int? id);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int? id);
    }
}
