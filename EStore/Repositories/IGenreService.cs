using EStore.Models;

namespace EStore.Repositories
{
    public interface IGenreService
    {
        bool Add(Category model);
        bool Update(Category model);
        bool Delete(int id);
        Category FindById(int id);
        IEnumerable<Category> GetAll();
    }
}
