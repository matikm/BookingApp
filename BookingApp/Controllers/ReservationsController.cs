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
using BookingApp.Interfaces;
using static BookingApp.Helper;

namespace BookingApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IObjectForRentRepository _objectForRentRepositorytory;
        private readonly ICustomerRepository _customerRepository;

        private ICollection<ObjectForRent> _objectForRents { get; set; }
        private ICollection<Customer> _customers { get; set; }
        public ReservationViewModel reservationViewModel { get; set; }

        public ReservationsController(IReservationRepository reservationRepository, ICustomerRepository customerRepository, IObjectForRentRepository objectForRentRepositorytory, BookingAppContext context)
        {
            _reservationRepository = reservationRepository;
            _objectForRentRepositorytory = objectForRentRepositorytory;
            _customerRepository = customerRepository;
            GetObjectAndCustomersAsync();
            reservationViewModel =  new ReservationViewModel();
        }

        void GetObjectAndCustomersAsync()
        {
            _objectForRents = _objectForRentRepositorytory.GetObjectForRents().Result;
            _customers = _customerRepository.GetCustomers().Result;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var t = DateTime.Now;
            reservationViewModel.fromDate = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0 );
            reservationViewModel.untilDate = new DateTime(t.Year, t.Month, t.Day, 23, 59, 59).AddMonths(1);
            reservationViewModel.Reservations = await _reservationRepository.GetReservations(reservationViewModel.fromDate, reservationViewModel.untilDate);
            reservationViewModel.ObjectForRents = new SelectList(_objectForRents, "Id", "Name");
            reservationViewModel.Customers = _customers.Select(a =>
                                              new SelectListItem
                                              {
                                                  Value = a.Id.ToString(),
                                                  Text = a.FirstName + " " + a.LastName
                                              });

            return View(reservationViewModel);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var reservation = await _reservationRepository.GetReservation(id);

            if (reservation == null)
                return NotFound();

            return View(reservation);
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel reservationViewModel)
        {
            Reservation reservation = reservationViewModel.Reservation;
            reservation.ObjectForRent = _objectForRents.FirstOrDefault(x => x.Id == reservationViewModel.ObjectForRentId);
            reservation.Customer = _customers.FirstOrDefault(x => x.Id == reservationViewModel.CustomerId);

            if (ModelState.IsValid)
            {
                await _reservationRepository.AddReservation(reservation);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var reservation = await _reservationRepository.GetReservation(id);
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
                return NotFound();

            if (ModelState.IsValid)
            {
                if (await _reservationRepository.UpdateReservation(reservation))
                    return RedirectToAction(nameof(Index));
                else 
                    return NotFound();
            }
            return View(reservation);
        }


        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id, DateTime fromDate, DateTime untilDate)
        {
            DetailsReservationViewModel DetailsReservationViewModel = new DetailsReservationViewModel();
            DetailsReservationViewModel.untilDate = untilDate;
            DetailsReservationViewModel.fromDate = fromDate;
            DetailsReservationViewModel.ObjectForRents = new SelectList(_objectForRents, "Id", "Name");
            DetailsReservationViewModel.Customers = _customers.Select(a =>
                                              new SelectListItem
                                              {
                                                  Value = a.Id.ToString(),
                                                  Text = a.FirstName + " " + a.LastName
                                              });

            if (id == 0)
            {
                DetailsReservationViewModel.Reservation = new Reservation();
                return View(DetailsReservationViewModel);
            }
            else
            {
                DetailsReservationViewModel.Reservation = await _reservationRepository.GetReservation(id);
                if (DetailsReservationViewModel.Reservation == null)
                {
                    return NotFound();
                }
                return View(DetailsReservationViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(DetailsReservationViewModel detailsReservationViewModel)
        {
            detailsReservationViewModel.Reservation.ObjectForRent = _objectForRents.FirstOrDefault(x => x.Id == detailsReservationViewModel.ObjectForRentId);
            detailsReservationViewModel.Reservation.Customer = _customers.FirstOrDefault(x => x.Id == detailsReservationViewModel.CustomerId);
            //Insert
            if (detailsReservationViewModel.Reservation.Id == 0)
            {
                await _reservationRepository.AddReservation(detailsReservationViewModel.Reservation);
            }
            //Update
            else
            {
                bool value = await _reservationRepository.UpdateReservation(detailsReservationViewModel.Reservation);
                if (value == false) return NotFound();
            }

            var FT = detailsReservationViewModel.fromDate;
            var UT = detailsReservationViewModel.untilDate;

            detailsReservationViewModel.fromDate = new DateTime(FT.Year, FT.Month, FT.Day, 0,0,0);
            detailsReservationViewModel.untilDate = new DateTime(UT.Year, UT.Month, UT.Day, 23, 59, 59);

            var reservations = await _reservationRepository.GetReservations(detailsReservationViewModel.fromDate, detailsReservationViewModel.untilDate);

            ReservationListViewModel reservationListViewModel = new ReservationListViewModel(reservations, FT, UT);
        
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "ReservationsList", reservationListViewModel) });
        }


























        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var reservation = await _reservationRepository.GetReservation(id);

            if (reservation != null)
                return View(reservation);
            else
                return NotFound();
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _reservationRepository.DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Reservations/GetForDate
        [HttpPost]
        public async Task<IActionResult> GetForDate(DateTime fromDate, DateTime untilDate)
        {
            reservationViewModel.Reservations = await _reservationRepository.GetReservations(fromDate, untilDate);
            return PartialView("ReservationsList", reservationViewModel.Reservations);
        }

        [HttpPost]
        public async Task<IActionResult> GetReservationDetails(int id)
        {
            reservationViewModel.Reservation = await _reservationRepository.GetReservation(id);
            return PartialView("Details", reservationViewModel.Reservations);
        }
    }
}
