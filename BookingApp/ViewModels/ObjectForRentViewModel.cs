using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public class ObjectForRentViewModel
    {
        public ObjectForRent ObjectForRent { get; set; }
        public ICollection<ObjectForRent> ObjectForRents { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
