using System.Text.Json.Serialization;
using BethaniShop.API.App;
using BethaniShop.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BethanyPiesShopDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BethanyPiesShopDbContextConnection' not found.");

builder.Services.AddSession();
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRazorPages();
//Support for Blazor and SSR
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
//shortcut
// builder.Services.AddScoped<IShoppingCart, ShoppingCart>(ShoppingCart.GetCart);
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(serviceProvider => ShoppingCart.GetCart(serviceProvider));

builder.Services.AddDbContext<BethanyPiesShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanyPiesShopDbContextConnection"]
    );
});

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<BethanyPiesShopDbContext>();

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.UseAntiforgery();
app.MapRazorPages();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

DbInitializer.Seed(app);

app.Run();