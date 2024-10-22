using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BethaniShop.API.Pages;

public class CheckoutComplete : PageModel
{
    public void OnGet()
    {
        ViewData["CheckoutCompleteMessage"] = "Thank you for yr order!";
    }
}