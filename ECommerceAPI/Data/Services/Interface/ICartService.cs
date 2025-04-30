using ECommerceAPI.Data.DTOs.CartDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Interface
{
    public interface ICartService
    {
        Task<List<CartItem>> GetAllCartItemsByUserId(string userId);
        Task<bool> AddToCart(CreateCartItemDTO model);
        Task DeleteCartItem(Guid CartItemId)
        Task ClearCart(string UserId);
    }
}
