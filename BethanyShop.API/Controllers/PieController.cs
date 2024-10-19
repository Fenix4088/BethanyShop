using BethaniShop.API.Models;
using BethaniShop.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniShop.API.Controllers;

public class PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository) : Controller
{

    private readonly IPieRepository _pieRepository = pieRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    
    public ViewResult List(string category)
    {
        IEnumerable<Pie> pies;
        string? currentCategory;

        if (string.IsNullOrEmpty(category))
        {
            pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
            currentCategory = "All pies";
        }
        else
        {
            pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                .OrderBy(p => p.PieId);
            currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
        }

        return View(new PieListViewModel(pies, currentCategory));
    }
    
    public IActionResult Details(int id)
    {
        var pie = _pieRepository.GetPieById(id);
        if(pie == null) return NotFound();
        return View(pie);
    }
}