namespace EStore.Repositories
{
    public interface ICartRepository
    {
        Task<bool> AddItem(int productId, int quantity);
        Task<bool> RemoveItem(int productId);
        Task<IEnumerable<ShoppingCart>> GetUserCart();
    }
}
