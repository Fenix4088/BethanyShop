using Microsoft.EntityFrameworkCore;

namespace BethaniShop.API.Models;

public class ShoppingCart(BethanyPiesShopDbContext bethanyPiesShopDbContext): IShoppingCart
{
        private readonly BethanyPiesShopDbContext _bethanyPiesShopDbContext = bethanyPiesShopDbContext;

        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        

        // It is a cart factory, wich will create a new all already excisting cart
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            BethanyPiesShopDbContext context = services.GetService<BethanyPiesShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem =
                _bethanyPiesShopDbContext.ShoppingCartItem.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _bethanyPiesShopDbContext.ShoppingCartItem.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bethanyPiesShopDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _bethanyPiesShopDbContext.ShoppingCartItem.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _bethanyPiesShopDbContext.ShoppingCartItem.Remove(shoppingCartItem);
                }
            }

            _bethanyPiesShopDbContext.SaveChanges();

            return localAmount;
        }

        
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                       _bethanyPiesShopDbContext.ShoppingCartItem.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _bethanyPiesShopDbContext
                .ShoppingCartItem
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _bethanyPiesShopDbContext.ShoppingCartItem.RemoveRange(cartItems);

            _bethanyPiesShopDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _bethanyPiesShopDbContext.ShoppingCartItem.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }

}