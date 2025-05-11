using ECommerceAPI.Data.DTOs.ProductImageDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Interface
{
    public interface IProductImageService
    {
        Task<List<string>> GetAllImagesByProductId(Guid productId);
        Task Upload(ProductImageDTO model);
        Task<bool> Delete(ProductImage image);
    }
}
