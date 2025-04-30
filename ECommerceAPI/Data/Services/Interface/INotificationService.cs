using ECommerceAPI.Data.DTOs.NotificationDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Interface
{
    public interface INotificationService
    {
        Task<Notification> GetById(Guid NotificationId);
        Task<List<Notification>> GetAllNotificationsByUserId(string userId);
        Task<Notification> Create(CreateNotificationDTO model);
        Task<Notification> Update(Notification Notification, UpdateNotificationDTO model);
        Task Delete(Notification Notification);
    }
}
