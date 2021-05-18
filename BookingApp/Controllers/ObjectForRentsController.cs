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

namespace BookingApp.Controllers
{
    public class ObjectForRentsController : Controller
    {
        private readonly IObjectForRentRepository _objectForRentRepositorytory;

        public ObjectForRentsController(IObjectForRentRepository objectForRentRepositorytory)
        {
            _objectForRentRepositorytory = objectForRentRepositorytory;
        }

        // GET: ObjectForRents
        public async Task<IActionResult> Index()
        {
            return View(await _objectForRentRepositorytory.GetObjectForRents());
        }

        // GET: ObjectForRents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var objectForRent = await _objectForRentRepositorytory.GetObjectForRent(id);

            if (objectForRent == null)
                return NotFound();

            return View(objectForRent);
        }

        // GET: ObjectForRents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ObjectForRents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Photo")] ObjectForRent objectForRent)
        {
            if (ModelState.IsValid)
            {
                await _objectForRentRepositorytory.AddObjectForRent(objectForRent);
                return RedirectToAction(nameof(Index));
            }
            return View(objectForRent);
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
            

            if (ModelState.IsValid)
            {
                if (await _objectForRentRepositorytory.UpdateObjectForRent(objectForRent)) 
                    return RedirectToAction(nameof(Index));
                else 
                    return NotFound();
            }
            return View(objectForRent);
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
