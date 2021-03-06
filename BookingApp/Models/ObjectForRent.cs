using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class ObjectForRent
    {
        public int ObjectForRentId { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
        [Display(Name = "Cennik")]
        public virtual ICollection<PricePerPeople> PriceList { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }


    }
}
