namespace ECommerceAPI.Models
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        // ShipmentStatus??
    }
}
