using BethaniShop.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BethaniShop.API.Pages;

public class CheckoutPage(IOrderRepository orderRepository, IShoppingCart shoppingCart) : PageModel
{
    
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IShoppingCart _shoppingCart = shoppingCart;
    
    [BindProperty]
    public Order Order { get; set; }

    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;

        if (_shoppingCart.ShoppingCartItems.Count == 0)
        {
            ModelState.AddModelError("", "Your cart is empty!");
        }

        if (ModelState.IsValid)
        {
            _orderRepository.CreateOrder(Order);
            _shoppingCart.ClearCart();
            return RedirectToAction("CheckoutComplete");
        }

        return Page();
    }
}