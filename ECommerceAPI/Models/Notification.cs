namespace ECommerceAPI.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        // type ????
    }
}
