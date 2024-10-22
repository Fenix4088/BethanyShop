namespace BethaniShop.API.Models;

public interface IOrderRepository
{
    void CreateOrder(Order order);
}