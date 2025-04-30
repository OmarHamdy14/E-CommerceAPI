using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.DTOs.CartDTOs;
using ECommerceAPI.Data.DTOs.NotificationDTOs;
using ECommerceAPI.Data.Services.Interface;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IEntityBaseRepository<CartItem> _base;
        public CartService(IEntityBaseRepository<CartItem> b)
        {
            _base = b;
        }
        public async Task<List<CartItem>> GetAllCartItemsByUserId(string userId)
        {
            return await _base.GetAll(n => n.UserId == userId);
        }
        public async Task<bool> AddToCart(CreateCartItemDTO model)
        {
            var IsExisted = await _base.Get(n => n.UserId == model.UserId && n.ProductId == model.ProductId);
            if (IsExisted is not null) { IsExisted.Quntity+=model.Quantity; await _base.Update(IsExisted); }
            else
            {
                var cartItem = new CartItem()
                {
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    Quntity = model.Quantity
                };
                await _base.Create(cartItem);
            }
            return true;
        }
        public async Task DeleteCartItem(Guid CartItemId)
        {
            var item = await _base.Get(n => n.Id == CartItemId); ;
            if (item.Quntity > 1) { item.Quntity--; await _base.Update(item); }
            else await _base.Remove(item);
        }
        public async Task ClearCart(string UserId)
        {
            var items = await GetAllCartItemsByUserId(UserId);
            await _base.RemoveRange(items);
        }

    }
}
