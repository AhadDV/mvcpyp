using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PypmvcTurtorial.Controllers
{
    public class ProductController : Controller
    {
        private readonly MvcDbContext _context;

        public ProductController(MvcDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
            => View(await _context.Products.ToListAsync());


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
                return NotFound();

            Product? product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
                return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (!ModelState.IsValid)
                 return View(product);

                _context.Update(product);
                await _context.SaveChangesAsync();
         
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
                 return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
                return Problem("Entity set 'MvcDbContext.Products'  is null.");

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

                _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string search)
        {
            if(string.IsNullOrWhiteSpace(search))
               return View(_context.Products.Where(x => x.Name.Contains(search)));

            return View(_context.Products.ToList());
        }
          
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
