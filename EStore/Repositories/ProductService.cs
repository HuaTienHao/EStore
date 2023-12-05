using EStore.Models;
using EStore.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace EStore.Repositories
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;
        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(Product model)
        {
            try
            {
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    var imageName = Path.GetFileName(model.ImageFile.FileName);
                    var fullPath = Path.Combine(imagePath, imageName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(stream);
                    }

                    model.Image =  imageName;
                }
                context.Products.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.Products.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Product FindById(int id)
        {
            return context.Products.Find(id);
        }

        public IEnumerable<Product> GetAll(string searchTerm = "")
        {
            searchTerm = searchTerm.ToLower();

            var data = (from product in context.Products
                        join category in context.Categories on product.CategoryId equals category.Id
                        where string.IsNullOrWhiteSpace(searchTerm) || product.ProductName.ToLower().Contains(searchTerm)
                        select new Product
                        {
                            Id = product.Id,
                            ProductName = product.ProductName,
                            Price = product.Price,
                            Image = product.Image,
                            CategoryId = category.Id,
                            CategoryName = category.CategoryName,
                            StoreName = product.StoreName,
                        }).ToList();

            return data;
        }


        public bool Update(Product model)
        {
            try
            {
                // Nếu người dùng đã chọn ảnh mới
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    // Kiểm tra xem tên ảnh mới có trùng với tên ảnh cũ hay không
                    if (Path.GetFileName(model.ImageFile.FileName) != Path.GetFileName(model.Image))
                    {
                        // Tên ảnh mới không trùng, lưu ảnh mới
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                        var imageName = Path.GetFileName(model.ImageFile.FileName);
                        var fullPath = Path.Combine(imagePath, imageName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            model.ImageFile.CopyTo(stream);
                        }

                        model.Image =  imageName;
                    }
                    // Nếu tên ảnh mới trùng với tên ảnh cũ, giữ nguyên ảnh cũ
                }
                context.Products.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
