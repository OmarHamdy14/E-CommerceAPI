using ECommerceAPI.Data.DTOs.CartDTOs;
using ECommerceAPI.Data.Services.Implementation;
using ECommerceAPI.Data.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [Authorize]
        [HttpGet("GetAllCartItemsByUserId/{UserId}")]
        public async Task<IActionResult> GetAllCartItemsByUserId(string UserId)
        {
            if (string.IsNullOrEmpty(UserId)) return BadRequest();
            try
            {
                var items = await _cartService.GetAllCartItemsByUserId(UserId);
                if (!items.Any()) return NotFound();

                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody]CreateCartItemDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var success = await _cartService.AddToCart(model);
                return success ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpDelete("DeleteCartItem/{CartItemId}")]
        public async Task<IActionResult> DeleteCartItem(Guid CartItemId)
        {
            try
            {
                await _cartService.DeleteCartItem(CartItemId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpDelete("ClearCart/{UserId}")]
        public async Task<IActionResult> ClearCart(string UserId)
        {
            try
            {
                await _cartService.ClearCart(UserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
