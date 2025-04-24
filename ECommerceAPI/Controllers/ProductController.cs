using ECommerceAPI.Data.DTOs.CategoryDTOs;
using ECommerceAPI.Data.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
        public ProductController(IProductService productService, IAccountService accountService)
        {
            _productService = productService;
            _accountService = accountService;
        }
        [Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();
            try
            {
                var product = await _productService.GetById(id);
                if (product == null) return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("GetAllCategoriesByUserId/{userId}")]
        public async Task<IActionResult> GetAllProductsByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user == null) return NotFound();

                var products = await _productService.GetAllProductsByUserId(userId);
                if (!products.Any()) return NotFound();

                return Ok(products);
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
                var product = await _productService.Create(model);
                return CreatedAtAction("GetAllProductsByUserId", new { userId = product.UserId }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("Update/{categoryId}")]
        public async Task<IActionResult> Update(int productId, [FromBody] UpdateCategoryDTO model)
        {
            if (productId <= 0) return BadRequest();
            try
            {
                var product = await _productService.GetById(productId);
                if (product is null) return NotFound();

                await _productService.Update(product, model);
                return Ok(new { Message = "Update is succeeded.", Product = product });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpDelete("Delete/{categoryId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            if (productId <= 0) return BadRequest();
            try
            {
                var product = await _productService.GetById(productId);
                if (product is null) return NotFound();

                await _productService.Delete(product);
                return Ok(new { Message = "Deletion is succeeded." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
