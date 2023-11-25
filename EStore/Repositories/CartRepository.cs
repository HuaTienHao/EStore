using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EStore.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AddItem(int productId, int quantity)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == productId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    var product = _db.Products.Find(productId);
                    cartItem = new CartDetail
                    {
                        ProductId = productId,
                        ShoppingCartId = cart.Id,
                        Quantity = quantity,
                        UnitPrice = product.Price // it is a new line after update
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
       
        }
        public async Task<bool> RemoveItem(int productId)
        {
            //using var transaction = _db.Database.BeginTransaction();
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return false;
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    return false;
                }
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == productId);
                if (cartItem is null)
                    return false;
                else if(cartItem.Quantity==1)
                { 
                    _db.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                _db.SaveChanges();
              //  transaction.Commit();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<IEnumerable<ShoppingCart>> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid userid");
            var shoppingCart = await _db.ShoppingCarts
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Product)
                                  .ThenInclude(a => a.Category)
                                  .Where(a => a.UserId == userId).ToListAsync();
            return shoppingCart;

        }
        public async Task<ShoppingCart> GetCart(string userId) 
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }


        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}

