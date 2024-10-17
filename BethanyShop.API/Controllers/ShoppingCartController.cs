using BethaniShop.API.Models;
using BethaniShop.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniShop.API.Controllers;

public class ShoppingCartController(IPieRepository pieRepository, IShoppingCart shoppingCart): Controller
{
    private readonly IPieRepository _pieRepository = pieRepository;
    private readonly IShoppingCart _shoppingCart = shoppingCart;


    public ViewResult Index()
    {
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;
        
        var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

        return View(shoppingCartViewModel);
    }
    
    public RedirectToActionResult AddToShoppingCart(int pieId)
    {
        var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

        if (selectedPie != null)
        {
            _shoppingCart.AddToCart(selectedPie);
        }

        return RedirectToAction("Index");
    }
    
    public RedirectToActionResult RemoveFromShoppingCart(int pieId)
    {
        var selectedPie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);

        if (selectedPie != null)
        {
            _shoppingCart.RemoveFromCart(selectedPie);
        }

        return RedirectToAction("Index");
    }
}