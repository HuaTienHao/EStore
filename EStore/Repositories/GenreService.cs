using EStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Repositories
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext context;
        public GenreService(ApplicationDbContext context)
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
    }
}
