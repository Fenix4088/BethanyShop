using BethaniShop.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethaniShop.API.Controllers;

public class OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart): Controller
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IShoppingCart _shoppingCart = shoppingCart;
    
    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;

        if (_shoppingCart.ShoppingCartItems.Count == 0)
        {
            ModelState.AddModelError("", "Your cart is empty!");
        }

        if (ModelState.IsValid)
        {
            _orderRepository.CreateOrder(order);
            _shoppingCart.ClearCart();
            return RedirectToAction("CheckoutComplete");
        }

        return View(order);
    }
    
    public IActionResult CheckoutComplete()
    {
        ViewBag.CheckoutCompleteMessage = "Thank u for yr order!";
        return View();
    }
}