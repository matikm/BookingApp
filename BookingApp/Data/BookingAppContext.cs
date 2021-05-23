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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PricePerPeople>()
                .HasOne(e => e.ObjectForRent)
                .WithMany(e => e.PriceList)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ObjectForRent>()
                .HasMany(b => b.PriceList)
                .WithOne(p => p.ObjectForRent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Reservation>()
                .HasOne(e => e.Customer)
                .WithMany(e => e.Reservations)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Customer>()
                .HasMany(b => b.Reservations)
                .WithOne(p => p.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Reservation>()
                .HasOne(e => e.ObjectForRent)
                .WithMany(e => e.Reservations)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ObjectForRent>()
                .HasMany(b => b.Reservations)
                .WithOne(p => p.ObjectForRent)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
