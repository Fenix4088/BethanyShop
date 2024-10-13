using Microsoft.EntityFrameworkCore;

namespace BethaniShop.API.Models;

public class PieRepository(BethanyPiesShopDbContext bethanyPiesShopDbContext): IPieRepository
{
    private readonly BethanyPiesShopDbContext _bethanyPiesShopDbContext = bethanyPiesShopDbContext;
    public IEnumerable<Pie> AllPies {
        get => _bethanyPiesShopDbContext.Pies.Include(pie => pie.Category);
    }
    public IEnumerable<Pie> PiesOfTheWeek => _bethanyPiesShopDbContext.Pies.Where(pie => pie.IsPieOfTheWeek);

    public Pie? GetPieById(int pieId)
    {
        return _bethanyPiesShopDbContext.Pies.Find(pieId);
    }
}