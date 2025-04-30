using ECommerceAPI.Data.DTOs.CategoryDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Interface
{
    public interface ICategoryService
    {
        Task<Category> GetById(Guid categoryId);
        Task<Category> Create(CreateCategoryDTO model);
        Task<Category> Update(Category category, UpdateCategoryDTO model);
        Task Delete(Category category);
    }
}
