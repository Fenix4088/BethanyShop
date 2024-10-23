using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BethaniShop.API.Models;

public class BethanyPiesShopDbContext(DbContextOptions<BethanyPiesShopDbContext> options): IdentityDbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Pie> Pies { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

}