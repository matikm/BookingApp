using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public class CustomerViewModel
    {
        public ICollection<Customer> Customers { get; set; }
        public Customer Customer { get; set; }
    }
}
