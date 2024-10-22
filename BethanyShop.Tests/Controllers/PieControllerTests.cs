using BethaniShop.API.Controllers;
using BethaniShop.API.ViewModels;
using BethanyShop.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BethanyShop.Tests.Controllers;

public class PieControllerTests
{
    [Fact]
    public void List_EmptyCategory_ReturnsAllPies()
    {
        //arrange
        var mockedPieRepository = RepositoryMocks.GetPieRepository();
        var mockedCategoryRepository = RepositoryMocks.GetCategoryRepository();
        var pieController = new PieController(mockedPieRepository.Object, mockedCategoryRepository.Object);
        
        //act
        var result = pieController.List("");
        
        //assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
        Assert.Equal(10, pieListViewModel.Pies.Count());
    }
}