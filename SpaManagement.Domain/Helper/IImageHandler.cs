using Microsoft.AspNetCore.Http;

namespace SpaManagement.Domain.Helper
{
    public interface IImageHandler
    {
        Task SaveImage(string path, List<IFormFile> files, string defaultNameFile = null);
    }
}