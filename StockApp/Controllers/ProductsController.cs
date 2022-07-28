using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockApp.Context;
using StockControl.Data.Operations;

namespace StockApp.Controllers
{
    public class ProductsController : Controller
    {
        
        private readonly DbOperations dbOps;

        public ProductsController(SDbContext context)
        {
            dbOps = new DbOperations(context);
        }

        
        public IActionResult Index()
        {
            return View(dbOps.ListProducts().ToList());
        }

        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = dbOps.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Piece")] Product product)
        {
            if (ModelState.IsValid)
            {
                dbOps.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = dbOps.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Price,Piece")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbOps.EditProduct(product);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = dbOps.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = dbOps.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return dbOps.ListProducts().Any(e => e.Id == id);
        }
    }
}
