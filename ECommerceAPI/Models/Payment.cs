namespace ECommerceAPI.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        // PaymentStatus??
    }
}
