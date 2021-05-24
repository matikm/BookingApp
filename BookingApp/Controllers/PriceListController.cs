using BookingApp.Interfaces;
using BookingApp.Models;
using BookingApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Controllers
{
    public class PriceListController : Controller
    {
        private readonly IPricePerPeopleRepository _pricePerPeopleRepository;
        private readonly IObjectForRentRepository _objectForRentRepositorytory;
        public PriceListViewModel PriceListViewModel { get; set; }

        public PriceListController(IPricePerPeopleRepository pricePerPeopleRepository, IObjectForRentRepository objectForRentRepositorytory)
        {
            PriceListViewModel = new PriceListViewModel();
            _pricePerPeopleRepository = pricePerPeopleRepository;
            _objectForRentRepositorytory = objectForRentRepositorytory;
        }

        // GET: PriceListController
        public async Task<ActionResult> Index(int Id)
        {
            PriceListViewModel.PriceList = await _pricePerPeopleRepository.GetPriceListForObject(Id);
            if (PriceListViewModel.PriceList != null)
                return View(PriceListViewModel);
            else
                return View();
        }

        // POST: PriceList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrEdit(PriceListViewModel priceListViewModel)
        {
            var pricePerPeople = priceListViewModel.PricePerPeople;
            string Message;
            //Insert
            if (pricePerPeople.PricePerPeopleId == 0)
            {
                pricePerPeople.ObjectForRent = await _objectForRentRepositorytory.GetObjectForRent(pricePerPeople.ObjectForRent.ObjectForRentId);
                await _pricePerPeopleRepository.AddPricePerPeople(pricePerPeople);
                Message = "Dodano";
            }
            //Update
            else
            {
                bool value = await _pricePerPeopleRepository.UpdatePricePerPeople(pricePerPeople);
                if (value == false) return NotFound();
                Message = "Edycja cennika przebiegła pomyślnie";
            }

            priceListViewModel.PriceList = await _pricePerPeopleRepository.GetPriceListForObject(pricePerPeople.ObjectForRent.ObjectForRentId);

            return Json(new { html = Helper.RenderRazorViewToString(this, "PriceList", priceListViewModel), message = Message, style = "success" });
        }

        // Get: PriceListController/Delete/5
        public async Task<ActionResult> Delete(int priceId, int objectId)
        {
            var isRemoved = await _pricePerPeopleRepository.DeletePricePerPeople(priceId);

            if (isRemoved)
            {
                PriceListViewModel.ObjectId = objectId;
                PriceListViewModel.PriceList = await _pricePerPeopleRepository.GetPriceListForObject(objectId);
                return Json(new { html = Helper.RenderRazorViewToString(this, "PriceList", PriceListViewModel) });
            }
            else
                return NotFound();
        }
    }
}
