using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApp.Data;
using BookingApp.Models;

namespace BookingApp.Controllers
{
    public class ObjectForRentsController : Controller
    {
        private readonly BookingAppContext _context;

        public ObjectForRentsController(BookingAppContext context)
        {
            _context = context;
        }

        // GET: ObjectForRents
        public async Task<IActionResult> Index()
        {
            return View(await _context.ObjectForRent.ToListAsync());
        }

        // GET: ObjectForRents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectForRent = await _context.ObjectForRent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objectForRent == null)
            {
                return NotFound();
            }

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
                _context.Add(objectForRent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(objectForRent);
        }

        // GET: ObjectForRents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectForRent = await _context.ObjectForRent.FindAsync(id);
            if (objectForRent == null)
            {
                return NotFound();
            }
            return View(objectForRent);
        }

        // POST: ObjectForRents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Photo")] ObjectForRent objectForRent)
        {
            if (id != objectForRent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objectForRent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObjectForRentExists(objectForRent.Id))
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
            return View(objectForRent);
        }

        // GET: ObjectForRents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectForRent = await _context.ObjectForRent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objectForRent == null)
            {
                return NotFound();
            }

            return View(objectForRent);
        }

        // POST: ObjectForRents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var objectForRent = await _context.ObjectForRent.FindAsync(id);
            _context.ObjectForRent.Remove(objectForRent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObjectForRentExists(int id)
        {
            return _context.ObjectForRent.Any(e => e.Id == id);
        }
    }
}
