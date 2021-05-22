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
        [Required(ErrorMessage = "This Field is required.")]
        public DateTime CheckIn { get; set; }
        [Display(Name = "Check Out")]
        [Required(ErrorMessage = "This Field is required.")]
        public DateTime CheckOut { get; set; }
        
        [Display(Name = "Ilość osób")]
        [Required(ErrorMessage = "This Field is required.")]
        public int People { get; set; }
        [Display(Name = "Płatość zaliczki")]
        [Required(ErrorMessage = "This Field is required.")]
        public bool ReservationDepositPayment { get; set; }
        [Display(Name = "Płatość całość")]
        public bool ReservationPricePayment { get; set; }
        [Display(Name = "Zaliczka")]
        [Required(ErrorMessage = "This Field is required.")]
        public int ReservationDeposit { get; set; }
        [Display(Name = "Całość")]
        [Required(ErrorMessage = "This Field is required.")]
        public int ReservationPrice { get; set; }
        [Display(Name = "Klient")]
        public virtual Customer Customer { get; set; }
        [Display(Name = "Objekt")] 
        public virtual ObjectForRent ObjectForRent { get; set; }
    }
}
