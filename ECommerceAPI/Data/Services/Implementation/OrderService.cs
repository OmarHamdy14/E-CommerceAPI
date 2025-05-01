using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.DTOs.OrderDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class OrderService
    {
        private readonly IEntityBaseRepository<Order> _base;
        private readonly IEntityBaseRepository<CartItem> _baseCartItem;
        private readonly IEntityBaseRepository<Product> _baseProduct;
        public OrderService(IEntityBaseRepository<Order> b, IEntityBaseRepository<CartItem> baseCartItem, IEntityBaseRepository<Product> baseProduct)
        {
            _base = b;
            _baseCartItem = baseCartItem;
            _baseProduct = baseProduct;
        }
        public async Task<List<Order>> GetAllOrdersByUserId(string userId)
        {
            return await _base.GetAll(n => n.UserId == userId);
        }
        public async Task<bool> Create(string userId,CreateOrderDTO model)
        {
            var cartItems = await _baseCartItem.GetAll(n => n.UserId == model.UserId);
            if (!cartItems.Any()) return false;

            var orderItems = new List<OrderItem>();
            decimal TotalPrice = 0;
            foreach(var item in cartItems)
            {
                var product = await _baseProduct.Get(p => p.Id==item.ProductId);
                var orderItem = new OrderItem()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = item.Quntity
                };
                TotalPrice += (item.Quntity * product.Price);
                orderItems.Add(orderItem);
            }

            var order = new Order()
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = TotalPrice,
                OrderItems = orderItems
            };

            await _base.Create(order);
            await _baseCartItem.RemoveRange(cartItems);
            return true;
        }

    }
}
