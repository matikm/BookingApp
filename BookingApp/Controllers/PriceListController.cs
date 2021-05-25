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
            PriceListViewModel.PricePerPeople = new PricePerPeople();
            _pricePerPeopleRepository = pricePerPeopleRepository;
            _objectForRentRepositorytory = objectForRentRepositorytory;
        }

        // GET: PriceListController
        public async Task<ActionResult> Index(int Id)
        {
            PriceListViewModel.PriceList = await _pricePerPeopleRepository.GetPriceListForObject(Id);
            PriceListViewModel.PricePerPeople.ObjectForRent = await _objectForRentRepositorytory.GetObjectForRent(Id);
            PriceListViewModel.ObjectId = Id;
            PriceListViewModel.PricePerPeople.People = 1;
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
            

            if (ModelState.IsValid)
            {
                //Insert
                if (pricePerPeople.PricePerPeopleId == 0)
                {
                    pricePerPeople.ObjectForRent = await _objectForRentRepositorytory.GetObjectForRent(pricePerPeople.ObjectForRent.ObjectForRentId);
                    priceListViewModel.PriceList = await _pricePerPeopleRepository.GetPriceListForObject(pricePerPeople.ObjectForRent.ObjectForRentId);
                    var Price = priceListViewModel.PriceList.FirstOrDefault(c => c.People == pricePerPeople.People);
                    if (Price != null)
                    {
                        Price.Price = pricePerPeople.Price;
                        await _pricePerPeopleRepository.UpdatePricePerPeople(Price);
                        Message = "Zaktualizowano cenę";
                    }
                    else
                    {
                        await _pricePerPeopleRepository.AddPricePerPeople(pricePerPeople);
                        Message = "Dodano";
                    }
                }
                //Update
                else
                {
                    bool value = await _pricePerPeopleRepository.UpdatePricePerPeople(pricePerPeople);
                    if (value == false) return NotFound();
                    Message = "Edycja cennika przebiegła pomyślnie";
                }

                priceListViewModel.PriceList = await _pricePerPeopleRepository.GetPriceListForObject(pricePerPeople.ObjectForRent.ObjectForRentId);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "PriceList", priceListViewModel), message = Message, style = "success" });
            }
            else
            {
                priceListViewModel.PriceList = await _pricePerPeopleRepository.GetPriceListForObject(pricePerPeople.ObjectForRent.ObjectForRentId);
                Message = "Wprowadz poprawne dane";
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "PriceList", priceListViewModel), message = Message, style = "error" });
            }
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
