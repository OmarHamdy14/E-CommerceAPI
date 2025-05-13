using AutoMapper;
using ECommerceAPI.Data.DTOs.AccountDTOs;
using ECommerceAPI.Data.DTOs.CartDTOs;
using ECommerceAPI.Data.DTOs.CategoryDTOs;
using ECommerceAPI.Data.DTOs.NotificationDTOs;
using ECommerceAPI.Data.DTOs.OrderDTOs;
using ECommerceAPI.Data.DTOs.ProductDTOs;
using ECommerceAPI.Data.DTOs.ProductImageDTOs;
using ECommerceAPI.Data.DTOs.Shipment;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, RegisterUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserDTO>().ReverseMap();

            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

            CreateMap<Notification, CreateNotificationDTO>().ReverseMap();
            CreateMap<Notification, UpdateNotificationDTO>().ReverseMap();

            CreateMap<Order, CreateOrderDTO>().ReverseMap();

            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();

            CreateMap<Shipment, CreateShipmentDTO>().ReverseMap();
        }
    }
}
