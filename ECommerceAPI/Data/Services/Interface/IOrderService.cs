using ECommerceAPI.Data.DTOs.OrderDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersByUserId(string userId);
        Task<bool> Create(string userId, CreateOrderDTO model);
    }
}
