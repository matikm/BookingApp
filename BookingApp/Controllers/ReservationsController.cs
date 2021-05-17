using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApp.Data;
using BookingApp.Models;
using BookingApp.ViewModels;

namespace BookingApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly BookingAppContext _context;
        private ICollection<ObjectForRent> _objectForRents;
        private ICollection<Customer> _customers;

        public ReservationsController(BookingAppContext context)
        {
            _context = context;
            _objectForRents = _context.ObjectForRent.ToList();
            _customers = _context.Customer.ToList();
        }

        // GET: Reservations
        public IActionResult Index()
        {
            ReservationListViewModel reservationListViewModel = new ReservationListViewModel();
            reservationListViewModel.Reservations = _context.Reservation.ToList();
            reservationListViewModel.ObjectForRents = new SelectList(_objectForRents, "Id", "Name");
            reservationListViewModel.Customers = _customers.Select(a =>
                                              new SelectListItem
                                              {
                                                  Value = a.Id.ToString(),
                                                  Text = a.FirstName + " " + a.LastName
                                              });

            return View(reservationListViewModel);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationListViewModel reservationListViewModel)
        {
            Reservation reservation = reservationListViewModel;
            reservation.ObjectForRent = _objectForRents.FirstOrDefault(x => x.Id == reservationListViewModel.ObjectForRentId);
            reservation.Customer = _customers.FirstOrDefault(x => x.Id == reservationListViewModel.CustomerId);

            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckIn,CheckOut,People,ReservationDepositPayment,ReservationPricePayment,ReservationDeposit,ReservationPrice")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
