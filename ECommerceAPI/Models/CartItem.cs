namespace ECommerceAPI.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
        public int Quntity { get; set; }
    }
}