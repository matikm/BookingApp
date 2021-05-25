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
using Ical.Net.Serialization;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net;
using System.Text;

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
        public ReservationViewModel reservationViewModel { get; set; } = new ReservationViewModel();

        public ReservationsController(IReservationRepository reservationRepository, IPricePerPeopleRepository pricePerPeopleRepository, ICustomerRepository customerRepository, IObjectForRentRepository objectForRentRepositorytory, BookingAppContext context)
        {
            _reservationRepository = reservationRepository;
            _objectForRentRepositorytory = objectForRentRepositorytory;
            _customerRepository = customerRepository;
            _pricePerPeopleRepository = pricePerPeopleRepository;
            GetObjectAndCustomersAsync(); 
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
                                    Value = a.CustomerId.ToString(),
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
            detailsReservationViewModel.Reservation.ObjectForRent = _objectForRents.FirstOrDefault(x => x.ObjectForRentId == detailsReservationViewModel.ObjectForRentId);
            detailsReservationViewModel.Reservation.Customer = _customers.FirstOrDefault(x => x.CustomerId == detailsReservationViewModel.CustomerId);

            string Message;
            //Insert
            if (detailsReservationViewModel.Reservation.ReservationId == 0)
            {
                await _reservationRepository.AddReservation(detailsReservationViewModel.Reservation);
                Message = "Dodano rezerwację";
            }
            //Update
            else
            {
                bool value = await _reservationRepository.UpdateReservation(detailsReservationViewModel.Reservation);
                if (value == false) return Json(new { message = "Edycja rezerwacji się nie powiódła", style = "error" });
                Message = "Edycja rezerwacji przebiegła pomyślnie";
            }

            var FT = detailsReservationViewModel.fromDate;
            var UT = detailsReservationViewModel.untilDate;

            detailsReservationViewModel.fromDate = new DateTime(FT.Year, FT.Month, FT.Day, 0,0,0);
            detailsReservationViewModel.untilDate = new DateTime(UT.Year, UT.Month, UT.Day, 23, 59, 59);

            var reservations = await _reservationRepository.GetReservations(detailsReservationViewModel.fromDate, detailsReservationViewModel.untilDate);

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "ReservationsList", new ReservationViewModel(reservations, FT, UT)), message = Message, style = "success" });
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, DateTime fromDate, DateTime untilDate)
        {
            var isRemoved = await _reservationRepository.DeleteReservation(id);

            if (isRemoved)
            {
                var FT = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
                var UT = new DateTime(untilDate.Year, untilDate.Month, untilDate.Day, 23, 59, 59);
                var reservations = await _reservationRepository.GetReservations(FT, UT);

                return Json(new { html = Helper.RenderRazorViewToString(this, "ReservationsList", new ReservationViewModel(reservations, FT, UT)) });
            }
            else
                return NotFound();
        }

        // POST: Reservations/GetForDate
        [HttpPost]
        public async Task<IActionResult> GetForDate(DateTime fromDate, DateTime untilDate)
        {
            reservationViewModel.Reservations = await _reservationRepository.GetReservations(fromDate, untilDate);
            reservationViewModel.untilDate = untilDate;
            reservationViewModel.fromDate = fromDate;
            return Json(new { html = Helper.RenderRazorViewToString(this, "ReservationsList", reservationViewModel), message = reservationViewModel.Reservations.Count() });
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

        // GET: Reservations/GetIcal
        public async Task<IActionResult> GetIcalAsync()
        {
            var Reservations = await _reservationRepository.GetReservations(DateTime.Now.AddMonths(-1));
            var calendar = new Calendar();

            foreach (var item in Reservations)
            {
                calendar.Events.Add(
                    new CalendarEvent
                    {
                        Summary = $"Rezerwacja {item.ObjectForRent.Name}",
                        Start = new CalDateTime(item.CheckIn),
                        End = new CalDateTime(item.CheckOut),
                        Description =   $"{item.Customer.FirstName} {item.Customer.LastName}\n" +
                                        $"Ilość osób: {item.People}\n" +
                                        $"Zaliczka: {item.ReservationDeposit}\n" +
                                        $"Całość: {item.ReservationPrice}\n",
                    }
                );
            }

            var serializer = new CalendarSerializer();
            var icalString = serializer.SerializeToString(calendar);

            return File(Encoding.UTF8.GetBytes(icalString), "text/plain", "Calendar.ics");
        }

    }
}
