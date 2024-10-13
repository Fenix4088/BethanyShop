namespace BethaniShop.API.Models;

public class CategoryRepository(BethanyPiesShopDbContext bethanyPiesShopDbContext): ICategoryRepository
{
    private readonly BethanyPiesShopDbContext _bethanyPiesShopDbContext = bethanyPiesShopDbContext;

    public IEnumerable<Category> AllCategories => _bethanyPiesShopDbContext.Categories.OrderBy(category => category.CategoryName);
}