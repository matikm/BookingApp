using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IPricePerPeopleRepository
    {
        Task<ICollection<PricePerPeople>> GetPricePerPeoples();
        Task<bool> AddPricePerPeople(PricePerPeople pricePerPeople);
        Task<PricePerPeople> GetPricePerPeople(int? id);
        Task<bool> UpdatePricePerPeople(PricePerPeople pricePerPeople);
        Task<bool> DeletePricePerPeople(int? id);
        Task<ICollection<PricePerPeople>> GetPriceListForObject(int? id);
    }
}
