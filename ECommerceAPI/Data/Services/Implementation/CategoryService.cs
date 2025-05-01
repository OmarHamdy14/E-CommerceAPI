using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.DTOs.CategoryDTOs;
using ECommerceAPI.Data.Services.Interface;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IEntityBaseRepository<Category> _base;
        private readonly IMapper _mapper;
        public CategoryService(IEntityBaseRepository<Category> b, IMapper mapper)
        {
            _base = b;
            _mapper = mapper;
        }
        public async Task<Category> GetById(Guid categoryId)
        {
            return await _base.Get(n => n.Id == categoryId);
        }
        public async Task<List<Category>> GetAll()
        {
            return await _base.GetAll();
        }
        public async Task<Category> Create(CreateCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);
            category.CreatedAt = DateTime.UtcNow;
            await _base.Create(category);
            return category;
        }
        public async Task<Category> Update(Category category, UpdateCategoryDTO model)
        {
            _mapper.Map(category, model);
            await _base.Update(category);
            return category;
        }
        public async Task Delete(Category category)
        {
            await _base.Remove(category);
        }
    }
}
