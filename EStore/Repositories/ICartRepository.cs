namespace EStore.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int productId, int quantity);
        Task<int> RemoveItem(int productId);
        Task<DiscountVM> GetUserCart(string discountCode = "");
        Task<int> GetCartItemCount(string userId = "" );
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout(int discount = 0);
    }
}
