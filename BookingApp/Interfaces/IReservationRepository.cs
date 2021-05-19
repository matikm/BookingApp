using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Interfaces
{
    public interface IReservationRepository
    {
        Task<ICollection<Reservation>> GetReservations();
        Task<ICollection<Reservation>> GetReservations(DateTime fromDate);
        Task<ICollection<Reservation>> GetReservations(DateTime fromDate, DateTime untilDate);
        Task<bool> AddReservation(Reservation reservation);
        Task<Reservation> GetReservation(int? id);
        Task<bool> UpdateReservation(Reservation reservation);
        Task<bool> DeleteReservation(int? id);
    }
}
