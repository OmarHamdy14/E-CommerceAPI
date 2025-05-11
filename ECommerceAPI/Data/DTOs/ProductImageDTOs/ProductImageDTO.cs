namespace ECommerceAPI.Data.DTOs.ProductImageDTOs
{
    public class ProductImageDTO
    {
        public IFormFile file { get; set; }
        public string AlText { get; set; }
        public Guid ProductId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; }
    }
}
