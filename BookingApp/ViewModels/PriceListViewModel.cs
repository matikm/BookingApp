using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public class PriceListViewModel
    {
        public ICollection<PricePerPeople> PriceList { get; set; }
        public PricePerPeople PricePerPeople { get; set; }
        public int ObjectId { get; set; }
    }
}
