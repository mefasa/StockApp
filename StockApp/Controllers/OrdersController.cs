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
    public class OrdersController : Controller
    {
        
        private readonly DbOperations dbOps;

        public OrdersController(SDbContext context)
        {
            dbOps = new DbOperations(context);
        }

        
        public IActionResult Index()
        {
            var model = dbOps.ListOrders().Select(s => new OrderViewModel() 
            {
                Id = s.Id, 
                Customer = s.Customer.Name, 
                Product = s.Product.Name, 
                CustomerId = s.CustomerId, 
                ProductId = s.ProductId, 
                ProdPrice = s.Product.Price, 
                OrderDate = s.OrderDate, 
                Piece = s.Piece 
            });
            return View("Index", model.ToList());
        }

        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vs = dbOps.GetOrder(id);
            if (vs == null)
            {
                return NotFound();
            }

            return View(vs);
        }

       
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(dbOps.ListCustomers(), "Id", "Name");
            ViewData["ProductId"] = new SelectList(dbOps.ListProducts(), "Id", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product,Customer,Piece,OrderDate")] OrderViewModel order)
        {
            try
            {
                Order order1 = new Order()
                {
                    Id = order.Id,
                    CustomerId = order.Customer,
                    ProductId = order.Product,
                    Piece = order.Piece,
                    OrderDate = DateTime.Now
                };
                dbOps.CreateOrder(order1);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewData["CustomerId"] = new SelectList(dbOps.ListCustomers(), "Id", "Name", order.Customer);
                ViewData["ProductId"] = new SelectList(dbOps.ListProducts(), "Id", "Name", order.Product);
                return View(order);
                
            }
            
        }

        
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vs = dbOps.GetOrder(id);
            if (vs == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(dbOps.ListCustomers(), "Id", "Name", vs.CustomerId);
            ViewData["ProductId"] = new SelectList(dbOps.ListProducts(), "Id", "Name", vs.ProductId);
            return View(vs);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ProductId,CustomerId,Piece,OrderDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbOps.EditOrder(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(dbOps.ListCustomers(), "Id", "Id", order.CustomerId);
            ViewData["ProductId"] = new SelectList(dbOps.ListProducts(), "Id", "Id", order.ProductId);
            return View(order);
        }

        
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = dbOps.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            dbOps.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(string id)
        {
            return dbOps.ListOrders().Any(e => e.Id == id);
        }
        [HttpPost]
        public List<OrderViewModel> ListOrdersDate(DateTime start, DateTime end) 
        {
            List<OrderViewModel> vs;
            try
            {
                var xx = dbOps.ListOrders().Where(x => x.OrderDate > start && x.OrderDate < end).ToList();
                vs = xx.Select(s => new OrderViewModel()
                {
                    Id = s.Id,
                    Customer = s.Customer.Name,
                    Product = s.Product.Name,
                    CustomerId = s.CustomerId,
                    ProductId = s.ProductId,
                    ProdPrice = s.Product.Price,
                    OrderDate = s.OrderDate,
                    Piece = s.Piece
                }).ToList(); 

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vs;
        }
    }
}
