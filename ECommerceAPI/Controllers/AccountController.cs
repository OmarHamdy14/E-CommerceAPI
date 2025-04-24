using ECommerceAPI.Data.DTOs.AccountDTOs;
using ECommerceAPI.Data.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [Authorize]
        [HttpGet("FindById/{userId}")]
        public async Task<IActionResult> FindById(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user is null) return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("FindByUserName/{name}")]
        public async Task<IActionResult> FindByUserName(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest();
            try
            {
                var user = await _accountService.FindByUserName(name);
                if (user is null) return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _accountService.GetAllUsers();
                if (!users.Any()) return NotFound();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
        {
            try
            {
                var authModel = await _accountService.Register(model);
                if (!authModel.IsAuthenticated) return BadRequest(authModel);
                return Ok(authModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LogInDTO model)
        {
            try
            {
                var authModel = await _accountService.GetTokenAsync(model);
                if (!authModel.IsAuthenticated) return BadRequest(authModel);
                return Ok(authModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("Update/{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] UpdateUserDTO model)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user is null) return NotFound();

                var result = await _accountService.Update(user, model);
                if (result.Succeeded) return Ok(new { Message = "Update is succeeded." });
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("ChangePassword/{userId}")]
        public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordDTO model)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user is null) return NotFound();

                var result = await _accountService.ChangePassword(user, model);
                if (result) return Ok(new { Message = "Changing Password is done successfully." });
                return Ok(new { Message = "Changing Password is not done." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
