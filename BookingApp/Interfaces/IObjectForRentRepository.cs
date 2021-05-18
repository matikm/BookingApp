using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IObjectForRentRepository
    {
        Task<ICollection<ObjectForRent>> GetObjectForRents();
        Task<bool> AddObjectForRent(ObjectForRent objectForRent);
        Task<ObjectForRent> GetObjectForRent(int? id);
        Task<bool> UpdateObjectForRent(ObjectForRent objectForRent);
        Task<bool> DeleteObjectForRent(int? id);
    }
}
