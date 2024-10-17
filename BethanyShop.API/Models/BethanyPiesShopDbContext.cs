using Microsoft.EntityFrameworkCore;

namespace BethaniShop.API.Models;

public class BethanyPiesShopDbContext(DbContextOptions<BethanyPiesShopDbContext> options): DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Pie> Pies { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

}