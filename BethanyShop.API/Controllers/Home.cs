using BethaniShop.API.Models;
using BethaniShop.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniShop.API.Controllers;

public class Home(IPieRepository pieRepository) : Controller
{
    private readonly IPieRepository _pieRepository = pieRepository;
    
    public IActionResult Index()
    {
        IEnumerable<Pie> piesOfTheWeek = _pieRepository.PiesOfTheWeek;
        return View(new HomeViewModel(piesOfTheWeek));
    }
}
