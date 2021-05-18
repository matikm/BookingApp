using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingApp.Models;

namespace BookingApp.Data
{
    public class BookingAppContext : DbContext
    {
        public BookingAppContext (DbContextOptions<BookingAppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<BookingApp.Models.Reservation> Reservation { get; set; }

        public DbSet<BookingApp.Models.Customer> Customer { get; set; }

        public DbSet<BookingApp.Models.ObjectForRent> ObjectForRent { get; set; }
        public DbSet<BookingApp.Models.PricePerPeople> PricePerPeople { get; set; }
    }
}
