

using Microsoft.EntityFrameworkCore;

namespace EStore.Repositories
{
    public class HomeRepositories : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepositories(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Category>> Categories()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProducts(string sTerm="", int categoryId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Product> products = await (from product in _db.Products
                            join category in _db.Categories
                            on product.CategoryId equals category.Id
                            where string.IsNullOrWhiteSpace(sTerm) ||(product != null && product.ProductName.ToLower().StartsWith(sTerm))
                            select new Product
                            {
                                Id = product.Id,
                                Image = product.Image,
                                StoreName = product.StoreName,
                                ProductName = product.ProductName,
                                CategoryId = product.CategoryId,
                                Price = product.Price,
                                CategoryName = category.CategoryName,
                            }
                            ).ToListAsync();
            if(categoryId > 0) 
            {
                products = products.Where(a=>a.CategoryId == categoryId).ToList();
            }

            return products; // 
        }
    }
}
