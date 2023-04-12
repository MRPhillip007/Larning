using BLL.Services.FileUploadService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;

namespace JsonAuthentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IFileUploadService _uploadService;
        public TestController(IFileUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpGet]
        public string GetString()
        {
            return "All is working fine :) ";
        }

        [HttpPost]
        public string UploadFile([FromForm] ProductEntity product) => _uploadService.Upload(product.File);
    }
}
