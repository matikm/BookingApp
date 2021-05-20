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
        public SelectList ObjectForRents { get; set; }
        public IEnumerable<SelectListItem> Customers { get; set; }
        public Reservation Reservation { get; set; }
        [Display(Name = "Klient")]
        public int CustomerId { get; set; }
        [Display(Name = "Objekt")]
        public int ObjectForRentId { get; set; }

        public DateTime fromDate { get; set; }
        public DateTime untilDate { get; set; }
    }
}
