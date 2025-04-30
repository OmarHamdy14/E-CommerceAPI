using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.DTOs.OrderDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class OrderService
    {
        private readonly IEntityBaseRepository<Order> _base;
        private readonly IEntityBaseRepository<CartItem> _baseCartItem;
        public OrderService(IEntityBaseRepository<Order> b, IEntityBaseRepository<CartItem> baseCartItem)
        {
            _base = b;
            _baseCartItem = baseCartItem;
        }
        public async Task<List<Order>> GetAllOrdersByUserId(string userId)
        {
            return await _base.GetAll(n => n.UserId == userId);
        }
        public async Task<bool> Create(CreateOrderDTO model)
        {
            var items = await _baseCartItem.GetAll(n => n.UserId == model.UserId);
            if (!items.Any()) return false;


        }

    }
}
