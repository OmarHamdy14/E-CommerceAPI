using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.DTOs.ProductImageDTOs;
using ECommerceAPI.Data.Services.Interface;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class ProductImageService : IProductImageService
    {
        private readonly IEntityBaseRepository<ProductImage> _base;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductImageService(IEntityBaseRepository<ProductImage> baseProductImage, IWebHostEnvironment webHostEnvironment)
        {
            _base = baseProductImage;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<List<string>> GetAllImagesByProductId(Guid productId)
        {
            return (await _base.GetAll(i => i.ProductId == productId)).Select(i => $"/{i.ImageUrl}").ToList();
        }
        public async Task Upload(ProductImageDTO model)
        {
            string folder = Path.Combine("Uploads", "Products", model.ProductId.ToString());
            string path = Path.Combine(_webHostEnvironment.WebRootPath ?? "wwwroot", folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.file.FileName);
            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                model.file.CopyTo(fileStream);
            }
            ProductImage productImage = new ProductImage()
            {
                ProductId = model.ProductId,
                AltText = model.AlText,
                IsPrimary = model.IsPrimary,
                DisplayOrder = model.DisplayOrder,
                ImageUrl = @"\" + folder + @"\" + fileName
            };
            await _base.Create(productImage);
        }
        public async Task<bool> Delete(ProductImage image)
        {
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath ?? "wwwroot", image.ImageUrl);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            await _base.Remove(image);
            return true;
        }
    }
}
