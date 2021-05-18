using BookingApp.Interfaces;
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
        public async Task<ActionResult> Create(PriceListViewModel priceListViewModel)
        {
            int Id = priceListViewModel.PricePerPeople.ObjectForRent.Id;

            if(Id == null) 
                return Redirect("~/");
            else
            {
                priceListViewModel.PricePerPeople.ObjectForRent = await _objectForRentRepositorytory.GetObjectForRent(Id);
                await _pricePerPeopleRepository.AddPricePerPeople(priceListViewModel.PricePerPeople);
                return RedirectToAction("index", new { id = Id });
            }
        }

        // Get: PriceListController/Delete/5
        public async Task<ActionResult> Delete(int id, int priceId)
        {
            var objectForRent = await _pricePerPeopleRepository.DeletePricePerPeople(priceId);
            return RedirectToAction("index", new { id = id });
        }
    }
}
