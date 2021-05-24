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

namespace BookingApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerViewModel customerViewModel { get; set; } = new CustomerViewModel();
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            customerViewModel.Customers = await _customerRepository.GetCustomers();
            return View(customerViewModel);
        }


        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Customer customer)
        {
            string Message;
            //Insert
            if (customer.CustomerId == 0)
            {
                await _customerRepository.AddCustomer(customer);
                Message = "Dodano klienta";
            }
            //Update
            else
            {
                bool value = await _customerRepository.UpdateCustomer(customer);
                if (value == false) return NotFound();
                Message = "Edycja klienta przebiegła pomyślnie";
            }

            var Customers = await _customerRepository.GetCustomers();

            return Json(new {html = Helper.RenderRazorViewToString(this, "CustomerList", Customers), message = Message, style = "success" });
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isRemoved = await _customerRepository.DeleteCustomer(id);
            
            if (isRemoved){
                var Customers = await _customerRepository.GetCustomers();
                return Json(new { html = Helper.RenderRazorViewToString(this, "CustomerList", Customers) });
            }
            else
                return NotFound();
        }
    }
}
