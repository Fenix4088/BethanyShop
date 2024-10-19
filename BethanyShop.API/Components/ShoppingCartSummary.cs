using BethaniShop.API.Models;
using BethaniShop.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniShop.API.Components;

public class ShoppingCartSummary(IShoppingCart shoppingCart): ViewComponent
{
    private readonly IShoppingCart _shoppingCart = shoppingCart;
    
    public IViewComponentResult Invoke()
    {
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;
        
        var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());
        
        return View(shoppingCartViewModel);
    }
}