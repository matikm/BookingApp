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

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Telephone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.AddCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
                return NotFound();
            
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Telephone")] Customer customer)
        {
            if (id != customer.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (await _customerRepository.UpdateCustomer(customer)) 
                    return RedirectToAction(nameof(Index));
                else return NotFound();
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            if(customer != null) 
                return View(customer); 
            else
                return NotFound();

        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _customerRepository.DeleteCustomer(id); 
            return RedirectToAction(nameof(Index));
        }
    }
}
