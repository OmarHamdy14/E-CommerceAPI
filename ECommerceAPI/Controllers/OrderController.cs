using ECommerceAPI.Data.DTOs.OrderDTOs;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        public OrderController(IOrderService orderService, IAccountService accountService)
        {
            _orderService = orderService;
            _accountService = accountService;
        }
        [Authorize]
        [HttpGet("GetAllOrdersByUserId/{UserId}")]
        public async Task<IActionResult> GetAllOrdersByUserId(string UserId) 
        { 
             if(string.IsNullOrEmpty(UserId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(UserId);
                if (user is null) return NotFound();

                return Ok(await _orderService.GetAllOrdersByUserId(UserId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPost("Create/{userId}")]
        public async Task<IActionResult> Create(string userId, [FromBody]CreateOrderDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var success = await _orderService.Create(userId, model);
                return success ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
