using EStore.Models;

namespace EStore.Repositories
{
    public interface IProductService
    {
        bool Add(Product model);
        bool Update(Product model);
        bool Delete(int id);
        Product FindById(int id);
        IEnumerable<Product> GetAll(string sTerm = "");
    }
}
