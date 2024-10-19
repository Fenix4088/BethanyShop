using BethaniShop.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethaniShop.API.Components;

public class CategoryMenu(ICategoryRepository categoryRepository): ViewComponent
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    
    public IViewComponentResult Invoke()
    {
        var categories = _categoryRepository.AllCategories.OrderBy(c => c.CategoryName);
        
        return View(categories);
    }
}