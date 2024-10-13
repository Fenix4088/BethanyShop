namespace BethaniShop.API.Models;

public interface ICategoryRepository
{
    IEnumerable<Category> AllCategories { get; }
}