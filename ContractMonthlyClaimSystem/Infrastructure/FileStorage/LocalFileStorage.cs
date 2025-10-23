using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ContractMonthlyClaimSystem.Infrastructure.FileStorage
{
    public class LocalFileStorage
    {
        private readonly string _baseFolder;

        public LocalFileStorage(IWebHostEnvironment env)
        {
            _baseFolder = Path.Combine(env.WebRootPath, "uploads", "documents");
            Directory.CreateDirectory(_baseFolder);
        }

        public string Save(IFormFile file)
        {
            var safeName = Path.GetFileName(file.FileName);
            var target = Path.Combine(_baseFolder, $"{Path.GetRandomFileName()}_{safeName}");
            using var stream = new FileStream(target, FileMode.Create);
            file.CopyTo(stream);
            return target;
        }
    }
}
