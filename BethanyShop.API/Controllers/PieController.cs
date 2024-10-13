using BethaniShop.API.Models;
using BethaniShop.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniShop.API.Controllers;

public class PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository) : Controller
{

    private readonly IPieRepository _pieRepository = pieRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    
    public IActionResult List()
    {
        PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "Cheese cakes");
        return View(pieListViewModel);
    }
}