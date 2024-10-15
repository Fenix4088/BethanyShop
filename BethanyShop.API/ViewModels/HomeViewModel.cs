using BethaniShop.API.Models;

namespace BethaniShop.API.ViewModels;

public class HomeViewModel(IEnumerable<Pie> piesOfTheWeek)
{
    public IEnumerable<Pie> PiesOfTheWeek { get; } = piesOfTheWeek;
}