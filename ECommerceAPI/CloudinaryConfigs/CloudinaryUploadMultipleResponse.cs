namespace ECommerceAPI.CloudinaryConfigs
{
    public class CloudinaryUploadMultipleResponse
    {
        public List<CloudinaryUploadResponse> UploadedPhotos { get; set; } = new();
        public bool AllSucceeded => UploadedPhotos.All(p => p.IsSuccess);
    }
}
