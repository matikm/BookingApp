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
        public ICollection<PricePerPeople> PricePerPeoples { get; set; }
        public ObjectForRentViewModel ObjectForRentViewModel { get; set; }

        public ObjectForRentsController(IObjectForRentRepository objectForRentRepositorytory, IPricePerPeopleRepository pricePerPeopleRepository)
        {
            _objectForRentRepositorytory = objectForRentRepositorytory;          
        }

        // GET: ObjectForRents
        public async Task<IActionResult> Index()
        {
            ObjectForRentViewModel = new ObjectForRentViewModel(){
                ObjectForRents = await _objectForRentRepositorytory.GetObjectForRents()
            };

            return View(ObjectForRentViewModel);
        }

        // POST: ObjectForRents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(ObjectForRent objectForRent)
        {
            string Message;
            //Insert
            if (objectForRent.ObjectForRentId == 0)
            {
                await _objectForRentRepositorytory.AddObjectForRent(objectForRent);
                Message = "Dodano obiekt";
            }
            //Update
            else
            {
                bool value = await _objectForRentRepositorytory.UpdateObjectForRent(objectForRent);
                if (value == false) return NotFound();
                Message = "Edycja rezerwacji przebiegła pomyślnie";
            }

            var Objects = await _objectForRentRepositorytory.GetObjectForRents();

            return Json(new {html = Helper.RenderRazorViewToString(this, "ObjectForRentList", Objects), message = Message, style = "success" });
        }

        // POST: ObjectForRents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isRemoved = await _objectForRentRepositorytory.DeleteObjectForRent(id);

            if (isRemoved)
            {
                var Objects = await _objectForRentRepositorytory.GetObjectForRents();
                return Json(new { html = Helper.RenderRazorViewToString(this, "ObjectForRentList", Objects) });
            }
            else
                return NotFound();
        }

    }
}
