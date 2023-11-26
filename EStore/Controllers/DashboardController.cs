using EStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;

namespace EStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _context.Categories.ToListAsync();
			IEnumerable<Product> products = await (from product in _context.Products
                        join category in _context.Categories
                        on product.CategoryId equals category.Id
                        select new Product
                        {
							Id = product.Id,
							Image = product.Image,
							StoreName = product.StoreName,
							ProductName = product.ProductName,
							CategoryId = product.CategoryId,
							Price = product.Price,
							CategoryName = category.CategoryName,
						}).ToListAsync();

            DashboardVM dashboardVM = new DashboardVM 
            { 
                Products = products,
                Categories = categories
            };
            return View(dashboardVM);
        }
    }
}
