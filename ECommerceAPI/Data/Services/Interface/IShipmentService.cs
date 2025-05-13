using ECommerceAPI.Data.DTOs.Shipment;
using ECommerceAPI.Helpers.Enums;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Interface
{
    public interface IShipmentService
    {
        Task<Shipment> GetById(Guid Id);
        Task<Shipment> GetByOrderId(Guid OrderId);
        Task<List<Shipment>> GetAll();
        Task<Shipment> Create(CreateShipmentDTO model);
        Task<bool> Update(Guid shipmentId, ShipmentStatus status);

    }
}
