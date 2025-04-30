namespace ECommerceAPI.Data.DTOs.CartDTOs
{
    public class CreateCartItemDTO
    {
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
