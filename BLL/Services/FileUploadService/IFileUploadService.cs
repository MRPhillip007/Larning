using Microsoft.AspNetCore.Http;

namespace BLL.Services.FileUploadService
{
    public interface IFileUploadService
    {
        string Upload(IFormFile file);
    }
}
