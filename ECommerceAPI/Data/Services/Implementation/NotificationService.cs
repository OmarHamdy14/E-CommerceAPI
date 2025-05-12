using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.DTOs.NotificationDTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class NotificationService
    {
        private readonly IEntityBaseRepository<Notification> _base;
        private readonly IMapper _mapper;
        public NotificationService(IEntityBaseRepository<Notification> b, IMapper mapper)
        {
            _base = b;
            _mapper = mapper;
        }
        public async Task<Notification> GetById(Guid NotificationId)
        {
            return await _base.Get(n => n.Id == NotificationId);
        }
        public async Task<List<Notification>> GetAllNotificationsByUserId(string userId)
        {
            return await _base.GetAll(n => n.UserId == userId);
        }
        public async Task<Notification> Create(CreateNotificationDTO model)
        {
            var Notification = _mapper.Map<Notification>(model);
            Notification.SentAt = DateTime.UtcNow;
            await _base.Create(Notification);
            return Notification;
        }
        public async Task<Notification> Update(Notification Notification, UpdateNotificationDTO model)
        {
            _mapper.Map(Notification, model);
            await _base.Update(Notification);
            return Notification;
        }
        public async Task Delete(Notification Notification)
        {
            await _base.Remove(Notification);
        }
    }
}
