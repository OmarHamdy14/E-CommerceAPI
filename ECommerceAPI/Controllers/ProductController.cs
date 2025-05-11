using ECommerceAPI.Data.DTOs.CategoryDTOs;
using ECommerceAPI.Data.DTOs.ProductDTOs;
using ECommerceAPI.Data.DTOs.ProductImageDTOs;
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
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService; 
        public ProductController(IProductService productService, IAccountService accountService, ICategoryService categoryService, IProductImageService productImageService)
        {
            _productService = productService;
            _accountService = accountService;
            _categoryService = categoryService;
            _productImageService = productImageService;
        }
        [Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //if (id <= 0) return BadRequest();
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
        [HttpGet("GetAllProductsByCategoryId/{CategoryId}")]
        public async Task<IActionResult> GetAllProductsByCategoryId(Guid CategoryId)
        {
            //if (string.IsNullOrEmpty(CategoryId)) return BadRequest();
            try
            {
                var category = await _categoryService.GetById(CategoryId);
                if (category == null) return NotFound();

                var products = await _productService.GetAllProductsByCategoryId(CategoryId);
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
        public async Task<IActionResult> Create([FromBody] CreateProductDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var product = await _productService.Create(model);
                return CreatedAtAction("GetAllProductsByCategoryId", new { CategoryId = product.CategoryId }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("Update/{categoryId}")]
        public async Task<IActionResult> Update(Guid productId, [FromBody] UpdateProductDTO model)
        {
            //if (productId <= 0) return BadRequest();
            try
            {
                if (!ModelState.IsValid) return BadRequest();
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
        [HttpPut("UploadImages")]
        public async Task<IActionResult> UploadImages([FromBody]ProductImageDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                await _productImageService.Upload(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpDelete("Delete/{categoryId}")]
        public async Task<IActionResult> Delete(Guid productId)
        {
            //if (productId <= 0) return BadRequest();
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
