using BethaniShop.API.Models;

namespace BethaniShop.API.ViewModels;

public class PieListViewModel(IEnumerable<Pie> pies, string? currentCategory)
{
    public IEnumerable<Pie> Pies { get; } = pies;
    public string? CurrentCategory { get; } = currentCategory;
}