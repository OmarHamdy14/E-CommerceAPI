namespace ECommerceAPI.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public int DisplayOrder { get; set; } 
        public bool IsPrimary { get; set; }   
    }
}