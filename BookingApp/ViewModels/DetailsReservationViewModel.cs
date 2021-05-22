using BookingApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public class DetailsReservationViewModel
    {
        public IEnumerable<ObjectForRent> ObjectForRents { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public Reservation Reservation { get; set; }
        public int CustomerId { get; set; }
        public int ObjectForRentId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime untilDate { get; set; }
    }
}
