using ECommerceAPI.Data.DTOs.Shipment;
using ECommerceAPI.Data.Services.Interface;
using ECommerceAPI.Helpers.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            return Ok(await _shipmentService.GetById(Id));
        }
        [HttpGet("GetByOrderId/{OrderId}")]
        public async Task<IActionResult> GetByOrderId(Guid OrderId)
        {
            return Ok(await _shipmentService.GetByOrderId(OrderId));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _shipmentService.GetAll());
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateShipmentDTO model)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _shipmentService.Create(model));
        }
        [HttpPatch("Update")]
        public async Task<IActionResult> Update(Guid shipmentId, [FromBody]ShipmentStatus status)
        {
            var res = await _shipmentService.Update(shipmentId, status);
            return res ? Ok() : BadRequest();
        }
    }
}
