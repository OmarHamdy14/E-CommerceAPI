using ECommerceAPI.Data.DTOs.NotificationDTOs;
using ECommerceAPI.Data.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _NotificationService;
        private readonly IAccountService _accountService;
        public NotificationController(INotificationService NotificationService, IAccountService accountService)
        {
            _NotificationService = NotificationService;
            _accountService = accountService;
        }
        [Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //if (id <= 0) return BadRequest();
            try
            {
                var Notification = await _NotificationService.GetById(id);
                if (Notification == null) return NotFound();

                return Ok(Notification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpGet("GetAllNotificationsByUserId/{userId}")]
        public async Task<IActionResult> GetAllNotificationsByUserId(string userId)
        {
            //if (string.IsNullOrEmpty(CategoryId)) return BadRequest();
            try
            {
                var user = await _accountService.FindById(userId);
                if (user == null) return NotFound();

                var Notifications = await _NotificationService.GetAllNotificationsByUserId(userId);
                if (!Notifications.Any()) return NotFound();

                return Ok(Notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDTO model)
        {
            try
            {
                var Notification = await _NotificationService.Create(model);
                return CreatedAtAction("GetAllNotificationsByUserId", new { userId = Notification.UserId }, Notification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpPut("Update/{NotificationId}")]
        public async Task<IActionResult> Update(Guid NotificationId, [FromBody] UpdateNotificationDTO model)
        {
            //if (NotificationId <= 0) return BadRequest();
            try
            {
                var Notification = await _NotificationService.GetById(NotificationId);
                if (Notification is null) return NotFound();

                await _NotificationService.Update(Notification, model);
                return Ok(new { Message = "Update is succeeded.", Notification = Notification });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
        [Authorize]
        [HttpDelete("Delete/{NotificationId}")]
        public async Task<IActionResult> Delete(Guid NotificationId)
        {
            //if (NotificationId <= 0) return BadRequest();
            try
            {
                var Notification = await _NotificationService.GetById(NotificationId);
                if (Notification is null) return NotFound();

                await _NotificationService.Delete(Notification);
                return Ok(new { Message = "Deletion is succeeded." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Something went wrong." });
            }
        }
    }
}
