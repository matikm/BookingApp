using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApp.Data;
using BookingApp.Models;
using BookingApp.Interfaces;
using BookingApp.ViewModels;

namespace BookingApp.Controllers
{
    public class ObjectForRentsController : Controller
    {
        private readonly IObjectForRentRepository _objectForRentRepositorytory;
        private readonly IPricePerPeopleRepository _pricePerPeopleRepository;
        public ICollection<PricePerPeople> PricePerPeoples { get; set; }
        public ObjectForRentViewModel ObjectForRentViewModel { get; set; }

        public ObjectForRentsController(IObjectForRentRepository objectForRentRepositorytory, IPricePerPeopleRepository pricePerPeopleRepository)
        {
            _objectForRentRepositorytory = objectForRentRepositorytory;
            _pricePerPeopleRepository = pricePerPeopleRepository;           
        }

        // GET: ObjectForRents
        public async Task<IActionResult> Index()
        {
            ObjectForRentViewModel = new ObjectForRentViewModel(){
                ObjectForRents = await _objectForRentRepositorytory.GetObjectForRents()
            };

            return View(ObjectForRentViewModel);
        }

        // GET: ObjectForRents/Details/5
        public async Task<IActionResult> Details(int? id)
        {         
            var objectForRent = await _objectForRentRepositorytory.GetObjectForRent(id);

            if (objectForRent == null)
                return NotFound();

            return View(objectForRent);
        }

        // POST: ObjectForRents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ObjectForRentViewModel CreateObjectForRentViewModel)
        {
            if (ModelState.IsValid)
            {
                await _objectForRentRepositorytory.AddObjectForRent(CreateObjectForRentViewModel.ObjectForRent);
                return RedirectToAction(nameof(Index));
            }
            return View(CreateObjectForRentViewModel.ObjectForRent);
        }

        // GET: ObjectForRents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var objectForRent = await _objectForRentRepositorytory.GetObjectForRent(id);

            if (objectForRent == null)
                return NotFound();

            return View(objectForRent);
        }

        // POST: ObjectForRents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Photo")] ObjectForRent objectForRent)
        {
            if (id != objectForRent.Id)
                return NotFound();
            else
            {
                if (await _objectForRentRepositorytory.UpdateObjectForRent(objectForRent))
                    return RedirectToAction(nameof(Index));
                else
                    return NotFound();
            }
        }

        // GET: ObjectForRents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var objectForRent = await _objectForRentRepositorytory.GetObjectForRent(id);
            if (objectForRent != null)
                return View(objectForRent);
            else
                return NotFound();
        }

        // POST: ObjectForRents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var objectForRent = await _objectForRentRepositorytory.DeleteObjectForRent(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
