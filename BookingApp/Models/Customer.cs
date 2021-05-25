using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class Customer
    {
        public string CustomerName { get { return FirstName + " " + LastName; } }

        public int CustomerId { get; set; }
        [Display(Name = "Imie")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        [Phone]
        public string Telephone { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
