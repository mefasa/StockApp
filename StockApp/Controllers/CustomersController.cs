using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockApp.Context;
using StockApp.Models;
using StockControl.Data.Operations;

namespace StockApp.Controllers
{
    public class CustomersController : Controller
    {
        //private readonly SDbContext _context;
        private readonly DbOperations dbOps;

        public CustomersController(SDbContext context)
        {
            dbOps = new DbOperations(context);
        }

        // GET: Customers
        public IActionResult Index()
        {
            var cus = dbOps.ListCustomers().ToList();
            IEnumerable<CustomerViewModel> model = cus.Select(s => new CustomerViewModel
            {
                Id = s.Id,
                Name = s.Name,
                BirthDate = s.BirthDate,
                City = s.City,
                Contact = s.Contact
            });
            return View("Index", model.ToList());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = dbOps.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Contact,City,BirthDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                dbOps.CreateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = dbOps.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Contact,City,BirthDate")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbOps.EditCustomer(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = dbOps.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            dbOps.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return dbOps.ListCustomers().Any(e => e.Id == id);
        }
    }
}
