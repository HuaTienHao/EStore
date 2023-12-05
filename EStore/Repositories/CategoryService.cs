using EStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Repositories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool Add(Category model)
        {
            try
            {
                context.Categories.Add(model);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
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
                context.Categories.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Category FindById(int id)
        {
            return context.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public bool Update(Category model)
        {
            try
            {
                context.Categories.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Category> GetAll(string searchTerm = "")
        {
            searchTerm = searchTerm.ToLower();
            var categories = context.Categories
                .Where(c => string.IsNullOrWhiteSpace(searchTerm) || c.CategoryName.ToLower().Contains(searchTerm))
                .ToList();

            return categories;
        }

    }
}
