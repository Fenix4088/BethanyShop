namespace BethaniShop.API.Models;

public class OrderRepository(BethanyPiesShopDbContext bethanyPieShopDbContext, IShoppingCart shoppingCart)
    : IOrderRepository
{
    private readonly BethanyPiesShopDbContext _bethanyPieShopDbContext = bethanyPieShopDbContext;
    private readonly IShoppingCart _shoppingCart = shoppingCart;

    public void CreateOrder(Order order)
    {
        order.OrderPlaced = DateTime.Now;

        List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
        order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

        order.OrderDetails = new List<OrderDetail>();

        //adding the order with its details

        foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
        {
            var orderDetail = new OrderDetail
            {
                Amount = shoppingCartItem.Amount,
                PieId = shoppingCartItem.Pie.PieId,
                Price = shoppingCartItem.Pie.Price
            };

            order.OrderDetails.Add(orderDetail);
        }

        _bethanyPieShopDbContext.Orders.Add(order);

        _bethanyPieShopDbContext.SaveChanges();
    }
}