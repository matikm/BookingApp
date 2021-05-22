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
        private readonly IPricePerPeopleRepository _pricePerPeopleRepository;

        private ICollection<ObjectForRent> _objectForRents { get; set; }
        private ICollection<Customer> _customers { get; set; }
        public ReservationViewModel reservationViewModel { get; set; }

        public ReservationsController(IReservationRepository reservationRepository, IPricePerPeopleRepository pricePerPeopleRepository, ICustomerRepository customerRepository, IObjectForRentRepository objectForRentRepositorytory, BookingAppContext context)
        {
            _reservationRepository = reservationRepository;
            _objectForRentRepositorytory = objectForRentRepositorytory;
            _customerRepository = customerRepository;
            _pricePerPeopleRepository = pricePerPeopleRepository;
            GetObjectAndCustomersAsync();
            reservationViewModel =  new ReservationViewModel();
        }

        void GetObjectAndCustomersAsync()
        {
            _objectForRents = _objectForRentRepositorytory.GetObjectForRents().Result;
            _customers = _customerRepository.GetCustomers().Result;
        }

        IEnumerable<SelectListItem> GetSelectListItemCustomers()
        {
            return _customers.Select(a =>
                                new SelectListItem
                                {
                                    Value = a.Id.ToString(),
                                    Text = a.FirstName + " " + a.LastName
                                });
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var t = DateTime.Now;
            reservationViewModel.fromDate = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0 );
            reservationViewModel.untilDate = new DateTime(t.Year, t.Month, t.Day, 23, 59, 59).AddMonths(1);
            reservationViewModel.Reservations = await _reservationRepository.GetReservations(reservationViewModel.fromDate, reservationViewModel.untilDate);

            return View(reservationViewModel);
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id, DateTime fromDate, DateTime untilDate)
        {
            DetailsReservationViewModel DetailsReservationViewModel = new DetailsReservationViewModel();
            DetailsReservationViewModel.untilDate = untilDate;
            DetailsReservationViewModel.fromDate = fromDate;
            DetailsReservationViewModel.Customers = _customers;
            DetailsReservationViewModel.ObjectForRents = _objectForRents;

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
            detailsReservationViewModel.Reservation.ObjectForRent = _objectForRents.FirstOrDefault(o => o.Id == detailsReservationViewModel.ObjectForRentId);

            //Insert
            if (detailsReservationViewModel.Reservation.Id == 0)
                await _reservationRepository.AddReservation(detailsReservationViewModel.Reservation);
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

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "ReservationsList", new ReservationViewModel(reservations, FT, UT)) });
        }

        [NoDirectAccess]
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
        public async Task<IActionResult> DeleteConfirmed(int id, DateTime fromDate, DateTime untilDate)
        {
            var isRemoved = await _reservationRepository.DeleteReservation(id);

            var FT = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
            var UT = new DateTime(untilDate.Year, untilDate.Month, untilDate.Day, 23, 59, 59);
            var reservations = await _reservationRepository.GetReservations(FT, UT);

            return Json(new { html = Helper.RenderRazorViewToString(this, "ReservationsList", new ReservationViewModel(reservations, FT, UT)) });
        }

        // POST: Reservations/GetForDate
        [HttpPost]
        public async Task<IActionResult> GetForDate(DateTime fromDate, DateTime untilDate)
        {
            reservationViewModel.Reservations = await _reservationRepository.GetReservations(fromDate, untilDate);
            reservationViewModel.untilDate = untilDate;
            reservationViewModel.fromDate = fromDate;
            return Json(new { html = Helper.RenderRazorViewToString(this, "ReservationsList", reservationViewModel)});
        }

        // POST: Reservations/GetForDate
        [HttpPost]
        public async Task<IActionResult> CalculatePrice(int objectId, int numberPeople, DateTime checkIn, DateTime checkOut)
        {
            int ReservationPrice = 0;
            int ReservationDeposit = 0;
            var PiceList = await _pricePerPeopleRepository.GetPriceListForObject(objectId);

            if (PiceList.Count() == 0)
                return Json(new { reservationPrice = 0, reservationDeposit = 0, message = "Brak cennika dla obiektu" }); 
            else
            {
                var PricePerPeople = PiceList.FirstOrDefault(p => p.People == numberPeople);
                if(PricePerPeople == null)
                    return Json(new { reservationPrice = 0, reservationDeposit = 0, message = "Brak ceny dla podanej ilości osób" });

                checkIn = new DateTime(checkIn.Year, checkIn.Month, checkIn.Day, 0, 0, 0);
                checkOut = new DateTime(checkOut.Year, checkOut.Month, checkOut.Day, 0, 0, 0);
                TimeSpan Days = checkOut - checkIn;
                ReservationPrice = (int)PricePerPeople.Price * Days.Days;
                ReservationDeposit = (int)(ReservationPrice * 0.2);
            }

            return Json(new { reservationPrice = ReservationPrice, reservationDeposit = ReservationDeposit });
        }

    }
}
