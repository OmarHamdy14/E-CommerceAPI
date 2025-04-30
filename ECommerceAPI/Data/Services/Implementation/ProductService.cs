using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.DTOs.CategoryDTOs;
using ECommerceAPI.Data.DTOs.ProductDTOs;
using ECommerceAPI.Data.Services.Interface;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IEntityBaseRepository<Product> _base;
        private readonly IMapper _mapper;
        public ProductService(IEntityBaseRepository<Product> b, IMapper mapper)
        {
            _base = b;
            _mapper = mapper;
        }
        public async Task<Product> GetById(Guid ProductId)
        {
            return await _base.Get(n => n.Id == ProductId);
        }
        public async Task<List<Product>> GetAllProductsByCategoryId(Guid CategoryId)
        {
            return await _base.GetAll(n => n.CategoryId == CategoryId);
        }
        public async Task<Product> Create(CreateProductDTO model)
        {
            var product = _mapper.Map<Product>(model);
            product.CreatedAt = DateTime.UtcNow;
            await _base.Create(product);
            return product;
        }
        public async Task<Product> Update(Product product, UpdateProductDTO model)
        {
            _mapper.Map(product, model);
            await _base.Update(product);
            return product;
        }
        public async Task Delete(Product product)
        {
            await _base.Remove(product);
        }
    }
}
