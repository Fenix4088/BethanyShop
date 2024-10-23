using BethaniShop.API.Models;
using Microsoft.AspNetCore.Components;

namespace BethaniShop.API.App.Pages
{
    public partial class PieCard
    {
        [Parameter]
        public Pie? Pie { get; set; }
    }
}
