using BookingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public class ReservationViewModel
    {
        public ICollection<Reservation> Reservations { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime untilDate { get; set; }

        public ReservationViewModel(){}
        public ReservationViewModel(ICollection<Reservation> reservations, DateTime fromDate, DateTime untilDate)
        {
            Reservations = reservations;
            this.fromDate = fromDate;
            this.untilDate = untilDate;
        }
    }
}
