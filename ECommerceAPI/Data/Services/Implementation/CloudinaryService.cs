using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ECommerceAPI.CloudinaryConfigs;
using ECommerceAPI.Data.Services.Interface;
using Microsoft.Extensions.Options;
using System.Security.Principal;

namespace ECommerceAPI.Data.Services.Implementation
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IOptions<CloudinarySettings> options;
        public CloudinaryService()
        {
            var acc = new Account()
            {
                Cloud = options.Value.CloudName,
                ApiKey = options.Value.APIKey,
                ApiSecret = options.Value.APISecret
            };
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<CloudinaryUploadResponse> UploadFile(IFormFile file)
        {
            if (file == null || file.Length < 0)
            {
                return new CloudinaryUploadResponse()
                {
                    IsSuccess = false,
                    Message = "Invalid File"
                };
            }
            var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream)
            };
            var UploadResult = await _cloudinary.UploadAsync(uploadParams);
            return new CloudinaryUploadResponse()
            {
                Url = UploadResult.SecureUri.AbsoluteUri,
                PublicId = UploadResult.PublicId,
                IsSuccess = true,
                Message = "Uploading image is succeeded"
            };
        }

        public async Task<CloudinaryUploadMultipleResponse> UploadFiles(List<IFormFile> files)
        {
            var response = new CloudinaryUploadMultipleResponse();

            if (files == null || files.Count == 0)
            {
                response.UploadedPhotos.Add(new CloudinaryUploadResponse
                {
                    IsSuccess = false,
                    Message = "No files provided."
                });
                return response;
            }

            foreach (var file in files)
            {
                if (file.Length == 0)
                {
                    response.UploadedPhotos.Add(new CloudinaryUploadResponse
                    {
                        IsSuccess = false,
                        Message = $"File '{file.FileName}' is empty."
                    });
                    continue;
                }

                try
                {
                    using var stream = file.OpenReadStream();

                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream)
                    };

                    var result = await _cloudinary.UploadAsync(uploadParams);

                    if (result.Error != null)
                    {
                        response.UploadedPhotos.Add(new CloudinaryUploadResponse
                        {
                            IsSuccess = false,
                            Message = result.Error.Message
                        });
                    }
                    else
                    {
                        response.UploadedPhotos.Add(new CloudinaryUploadResponse
                        {
                            Url = result.SecureUrl.AbsoluteUri,
                            PublicId = result.PublicId,
                            IsSuccess = true
                        });
                    }
                }
                catch (Exception ex)
                {
                    response.UploadedPhotos.Add(new CloudinaryUploadResponse
                    {
                        IsSuccess = false,
                        Message = $"Unexpected error uploading '{file.FileName}': {ex.Message}"
                    });
                }
            }
            return response;
        }

        public async Task<CloudinaryDeleteResponse> DeleteFile(string FileId)
        {
            if (string.IsNullOrEmpty(FileId))
            {
                return new CloudinaryDeleteResponse()
                {
                    IsSuccess = false,
                    Message = "FileId is null"
                };
            }
            var deleteParams = new DeletionParams(FileId);
            var res = await _cloudinary.DestroyAsync(deleteParams);
            if (res.Result == "deleted")
            {
                return new CloudinaryDeleteResponse()
                {
                    IsSuccess = true,
                    Message = "Deletion is done"
                };
            }
            return new CloudinaryDeleteResponse()
            {
                IsSuccess = false,
                Message = $"Failed to delete this image!! {res.Error.Message}"
            };
        }
    }
}
