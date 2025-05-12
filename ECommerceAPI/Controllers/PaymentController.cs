using ECommerceAPI.Data.Services.Implementation;
using ECommerceAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PayPalService _payPalService;

        public PaymentController(IConfiguration configuration)
        {
            var client = PayPalClientFactory.CreatePayPalClient(configuration);
            _payPalService = new PayPalService(client);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromQuery] decimal amount)
        {
            var (orderId, approvalLink) = await _payPalService.CreateOrder(amount, "USD", "http://localhost:7164/api/payments/complete", "http://localhost:7164/api/payments/cancel");
            return Ok(new { OrderId = orderId, ApprovalLink = approvalLink });
        }

        [HttpGet("complete")]
        public async Task<IActionResult> CompleteOrder([FromQuery] string token)
        {
            var success = await _payPalService.CaptureOrder(token);
            return success ? Ok("Payment completed successfully.") : BadRequest("Payment capture failed.");
        }

        [HttpGet("cancel")]
        public IActionResult CancelOrder()
        {
            return BadRequest("Payment was canceled by the user.");
        }
    }
}
