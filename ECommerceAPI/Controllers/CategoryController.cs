using ECommerceAPI.Data.DTOs.CategoryDTOs;
using ECommerceAPI.Data.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        public CategoryController(ICategoryService categoryService, IAccountService accountService)
        {
            _categoryService = categoryService;
            _accountService = accountService;
        }
        [Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();
            try
            {
                var category = await _categoryService.GetById(id);
                if (category == null) return NotFound();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("GetAllCategoriesByUserId/{userId}")]
        public async Task<IActionResult> GetAllCategoriesByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user == null) return NotFound();

                var categories = await _categoryService.GetAllCategoriesByUserId(userId);
                if (!categories.Any()) return NotFound();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO model)
        {
            try
            {
                var category = await _categoryService.Create(model);
                return CreatedAtAction("GetAllCategoriesByUserId", new { userId = category.UserId }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("Update/{categoryId}")]
        public async Task<IActionResult> Update(int categoryId, [FromBody] UpdateCategoryDTO model)
        {
            if (categoryId <= 0) return BadRequest();
            try
            {
                var category = await _categoryService.GetById(categoryId);
                if (category is null) return NotFound();

                await _categoryService.Update(category, model);
                return Ok(new { Message = "Update is succeeded.", Category = category });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpDelete("Delete/{categoryId}")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            if (categoryId <= 0) return BadRequest();
            try
            {
                var category = await _categoryService.GetById(categoryId);
                if (category is null) return NotFound();

                await _categoryService.Delete(category);
                return Ok(new { Message = "Deletion is succeeded." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
