using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.DTOs.Shipment;
using ECommerceAPI.Data.Services.Interface;
using ECommerceAPI.Helpers.Enums;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class ShipmentService : IShipmentService
    {
        private readonly IEntityBaseRepository<Shipment> _base;
        private readonly IMapper _mapper;
        public ShipmentService(IEntityBaseRepository<Shipment> @base, IMapper mapper)
        {
            _base = @base;
            _mapper = mapper;
        }
        public async Task<Shipment> GetById(Guid Id)
        {
            return await _base.Get(s => s.Id == Id);
        }
        public async Task<Shipment> GetByOrderId(Guid OrderId)
        {
            return await _base.Get(s => s.OrderId == OrderId);
        }
        public async Task<List<Shipment>> GetAll()
        {
            return await _base.GetAll();
        }
        public async Task<Shipment> Create(CreateShipmentDTO model)
        {
            var shipment = _mapper.Map<Shipment>(model);
            shipment.CreatedAt = DateTime.UtcNow;
            await _base.Create(shipment);
            return shipment;
        }
        public async Task<bool> Update(Guid shipmentId, ShipmentStatus status)
        {
            var shipment = await _base.Get(s => s.Id == shipmentId);
            if (shipment is null) return false;

            shipment.Staus = status;
            await _base.Update(shipment);
            return true;
        }
    }
}
