using BethaniShop.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethaniShop.API.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class SearchController(IPieRepository pieRepository): ControllerBase
{
    private readonly IPieRepository _pieRepository = pieRepository;

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_pieRepository.AllPies);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (_pieRepository.AllPies.All(p => p.PieId != id))
        {
            return NotFound();
        }

        return Ok(_pieRepository.AllPies.Where(p => p.PieId == id));
    }
    
    [HttpPost]
    public IActionResult SearchPies([FromBody] string searchQuery)
    {
        IEnumerable<Pie> pies = new List<Pie>();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            pies = _pieRepository.SearchPies(searchQuery);
        }

        return new JsonResult(pies);
    }
}