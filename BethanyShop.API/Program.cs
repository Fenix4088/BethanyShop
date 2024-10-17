using BethaniShop.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();

//shortcut
// builder.Services.AddScoped<IShoppingCart, ShoppingCart>(ShoppingCart.GetCart);
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(serviceProvider => ShoppingCart.GetCart(serviceProvider));

builder.Services.AddDbContext<BethanyPiesShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanyPiesShopDbContextConnection"]
    );
});

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.MapDefaultControllerRoute();


DbInitializer.Seed(app);

app.Run();