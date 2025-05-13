using ECommerceAPI.Helpers.Enums;

namespace ECommerceAPI.Models
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public DateTime CreatedAt { get; set; }
        public ShipmentStatus Staus { get; set; }
        public Order Order { get; set; }
    }
}
