using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.Services.FileUploadService
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly AppDbContext _context;
        public FileUploadService(AppDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            // Injecting database context and enviroment
            _context = dbContext;
            _hostingEnvironment = hostingEnvironment;

        }
        public string Upload(IFormFile file)
        {
            string contentPath = _hostingEnvironment.ContentRootPath;
            string combinedPath = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(combinedPath))
            {
                Directory.CreateDirectory(combinedPath);
            }

            // check allowed file extension 
            string fileExtension = Path.GetExtension(file.FileName);
            string[] allowedExtensions = new string[] { ".jpg", ".png", ".txt" };

            if (!allowedExtensions.Contains(fileExtension))
            {
                return $"Files only with {string.Join(",", allowedExtensions)} are allowed! ";
            }

            string uniqueName = Guid.NewGuid().ToString();
            string renamedFile = uniqueName + fileExtension;
            string filePath = Path.Combine(combinedPath, renamedFile);

            var stream = File.OpenWrite(filePath);
            file.CopyTo(stream);
            stream.Close();

            return $"File {filePath} saved successfully! ";

            // maybe add some try catch block soon

        }
    }
}
