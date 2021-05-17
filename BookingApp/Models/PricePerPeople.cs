using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class PricePerPeople
    {
        public int Id { get; set; }
        [Display(Name = "Objekt")]
        public virtual ObjectForRent ObjectForRent { get; set; }
        [Display(Name = "Ilość osób")]
        public int People { get; set; }
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
    }
}
