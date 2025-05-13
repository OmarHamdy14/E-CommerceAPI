using ECommerceAPI.Helpers.Enums;

namespace ECommerceAPI.Data.DTOs.Shipment
{
    public class CreateShipmentDTO
    {
        public Guid OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public ShipmentStatus Staus { get; set; } = ShipmentStatus.Pending;
    }
}
