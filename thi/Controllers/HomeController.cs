using HoangHuyHieu_211243214.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HoangHuyHieu_211243214.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HoangHuyHieu_211243214.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        LtWeb_Thi_ShopContext _context = new LtWeb_Thi_ShopContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Product> top6 = _context.Products
                         .OrderByDescending(h => h.ProviderId)  
                         .Take(6)            
                         .ToList();
            return View(top6);
        }
        public IActionResult Search(string? search)
        {
            var list = _context.Products.Where(m => m.Name.ToLower().Contains(search.ToLower())).ToList();
            return PartialView("Product", list);
        }
		// GET: Products/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
			ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Id", product.ProviderId);
			return View(product);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, [Bind("Id,Name,UnitPrice,Image,Available,CategoryId,Description,ProviderId")] Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(product);
					await _context.SaveChangesAsync();
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
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
			ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Id", product.ProviderId);
			return View(product);
		}
		private bool ProductExists(string id)
		{
			return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
		}
		//public IActionResult Privacy()
		//{
		//    return View();
		//}

		//[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		//public IActionResult Error()
		//{
		//    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		//}
	}
}