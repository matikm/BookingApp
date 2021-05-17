using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Display(Name = "Check In")]
        public DateTime CheckIn { get; set; }
        [Display(Name = "Check Out")]
        public DateTime CheckOut { get; set; }
        [Display(Name = "Ilość osób")]
        public int People { get; set; }
        [Display(Name = "Płatość zaliczki")]
        public bool ReservationDepositPayment { get; set; }
        [Display(Name = "Płatość całość")]
        public bool ReservationPricePayment { get; set; }
        [Display(Name = "Zaliczka")]
        public decimal ReservationDeposit { get; set; }
        [Display(Name = "Całość")]
        public decimal ReservationPrice { get; set; }
        [Display(Name = "Klient")]
        public virtual Customer Customer { get; set; }
        [Display(Name = "Objekt")]
        public virtual ObjectForRent ObjectForRent { get; set; }
    }
}
