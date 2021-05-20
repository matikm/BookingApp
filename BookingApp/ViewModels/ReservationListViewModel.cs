using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public class ReservationListViewModel
    {
        public ICollection<Reservation> Reservations { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime untilDate { get; set; }


        public ReservationListViewModel(ICollection<Reservation> reservations, DateTime fromDate, DateTime untilDate)
        {
            Reservations = reservations;
            this.fromDate = fromDate;
            this.untilDate = untilDate;
        }
    }
}
