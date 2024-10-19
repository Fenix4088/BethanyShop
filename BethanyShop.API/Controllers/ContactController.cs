using Microsoft.AspNetCore.Mvc;

namespace BethaniShop.API.Controllers;

public class ContactController: Controller
{
    public IActionResult Index()
    {
        return View();
    }
}