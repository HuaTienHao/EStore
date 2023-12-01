using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EStore.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductDetailController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int productId)
        {
            var getproduct = await (from product in _context.Products
                                 join category in _context.Categories
                                 on product.CategoryId equals category.Id
                                 where product.Id == productId
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

            Product productdetail = getproduct[0];

            return View(productdetail);
        }
    }
}
