namespace ECommerceAPI.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        // OrderStatus?
    }
}
