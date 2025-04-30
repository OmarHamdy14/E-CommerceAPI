using ECommerceAPI.Data.DTOs.ProductDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Interface
{
    public interface IProductService
    {
        Task<Product> GetById(Guid ProductId);
        Task<List<Product>> GetAllProductsByCategoryId(Guid CategoryId);
        Task<Product> Create(CreateProductDTO model);
        Task<Product> Update(Product Product, UpdateProductDTO model);
        Task Delete(Product Product);
    }
}
