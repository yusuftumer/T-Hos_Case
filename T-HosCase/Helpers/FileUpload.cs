using Microsoft.Extensions.Configuration;
using T_HosCase.Helpers;

namespace T_HosCase.Helpers
{
    public class FileUpload : IFileUpload
    {
        private string[] FileExtensions = { ".png", ".jpg", ".jpeg", ".ico" };
        private string[] VideoExtensions = { ".mp4" };
        private string[] DocumentExtensions = { ".pdf", ".txt", ".docx", ".doc" };
        private readonly IWebHostEnvironment _webHost;

        public FileUpload(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public string DeleteImage(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return "Resim Bulunamadı";
            }
            string deleted = Path.Combine(_webHost.WebRootPath, "images\\*", Name);
            if (File.Exists(deleted))
            {
                File.Delete(deleted);
            }
            return "Resim Silindi";
        }

        public string DeleteImages(List<string> images)
        {
            if (images.Count > 0)
            {
                foreach (var item in images)
                {
                    var deleted = DeleteImage(item);
                }
            }
            return "Resimler Silindi";
        }

        public List<string> FileListUpload(List<IFormFile> files)
        {
            var imageUrlList = new List<string>();
            List<string> Failed = new List<string> { "Failed" };
            foreach (var file in files)
            {
                string UrlPath = "";
                string FileName = "";
                try
                {
                    ImageSave(file, ref UrlPath, ref FileName);
                    imageUrlList.Add(UrlPath);
                }
                catch (Exception)
                {

                    return Failed;
                }
            }
            return imageUrlList;
        }

        public string UpdateFile(IFormFile file, string Name)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                ImageSave(file, ref urlPath, ref fileName);

            }
            catch (Exception)
            {

                return "Failed";
            }

            string _imageToBeDeleted = Path.Combine(_webHost.WebRootPath, "images\\", Name);
            if ((File.Exists(_imageToBeDeleted)))
            {
                File.Delete(_imageToBeDeleted);
            }

            return $"{urlPath}";
        }
        private void ImageSave(IFormFile file, ref string UrlPath, ref string FileName)
        {
            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && FileExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileName = file.FileName;
                var path = Path.Combine(_webHost.WebRootPath, $"Images\\{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }
        private void ImageSaveAdvert(IFormFile file, ref string UrlPath, ref string FileName)
        {
            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && FileExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images/Adverts/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileName = file.FileName;
                var path = Path.Combine(_webHost.WebRootPath, $"Images\\{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }
        private void ImageSavePage(IFormFile file, ref string UrlPath, ref string FileName)
        {
            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && FileExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images/Pages/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileName = file.FileName;
                var path = Path.Combine(_webHost.WebRootPath, $"Images/Pages\\{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }
        private void ImageSaveStaff(IFormFile file, ref string UrlPath, ref string FileName)
        {
            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && FileExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images/Staff/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileName = file.FileName;
                var path = Path.Combine(_webHost.WebRootPath, $"Images/Staff\\{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }
        private void ImageSaveBlog(IFormFile file, ref string UrlPath, ref string FileName)
        {
            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && FileExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images/Blog/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileName = file.FileName;
                var path = Path.Combine(_webHost.WebRootPath, $"Images/Blog\\{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }
        private void ImageSaveBanner(IFormFile file, ref string UrlPath, ref string FileName)
        {
            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && FileExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images/Banners/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileName = file.FileName;
                var path = Path.Combine(_webHost.WebRootPath, $"Images/Banners\\{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }
        public string FileUploadsBlog(IFormFile file)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                ImageSaveBlog(file, ref urlPath, ref fileName);
            }
            catch (Exception)
            {

                return "Failed";
            }
            return $"{urlPath}";
        }
        public string FileUploadsBanner(IFormFile file)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                ImageSaveBanner(file, ref urlPath, ref fileName);
            }
            catch (Exception)
            {

                return "Failed";
            }
            return $"{urlPath}";
        }
        public string FileUploadsAdvert(IFormFile file)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                ImageSaveAdvert(file, ref urlPath, ref fileName);
            }
            catch (Exception)
            {

                return "Failed";
            }
            return $"{urlPath}";
        }
        public string FileUploadsPage(IFormFile file)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                ImageSavePage(file, ref urlPath, ref fileName);
            }
            catch (Exception)
            {

                return "Failed";
            }
            return $"{urlPath}";
        }
        public string FileUploadsStaff(IFormFile file)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                ImageSaveStaff(file, ref urlPath, ref fileName);
            }
            catch (Exception)
            {

                return "Failed";
            }
            return $"{urlPath}";
        }
        public string FileUploads(IFormFile file)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                ImageSave(file, ref urlPath, ref fileName);
            }
            catch (Exception)
            {

                return "Failed";
            }
            return $"{urlPath}";
        }
        public string VideoUploads(IFormFile file)
        {
            string urlPath = "";
            string fileName = "";
            try
            {
                VideoSave(file, ref urlPath, ref fileName);
            }
            catch (Exception)
            {

                return "Failed";
            }
            return $"{urlPath}";
        }
        private void VideoSave(IFormFile file, ref string UrlPath, ref string FileName)
        {

            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && VideoExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileName = file.FileName;
                var path = Path.Combine(_webHost.WebRootPath, $"images/\\", $"{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }
        public string DocumentUploads(IFormFile files)
        {

            string Failed = new string("Failed");

            string UrlPath = "";
            string FileName = "";
            try
            {
                DocumentSave(files, ref UrlPath, ref FileName);

            }
            catch (Exception)
            {

                return Failed;
            }

            return $"{UrlPath}";


        }
        private void DocumentSave(IFormFile file, ref string UrlPath, ref string FileName)
        {

            var guid = Guid.NewGuid();
            var subGuid = guid.ToString().Substring(0, 13);
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file != null && DocumentExtensions.Contains(fileExt))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Panel\\AdddocPanel/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileName = file.FileName;
                var path = Path.Combine(_webHost.WebRootPath, $"Panel\\AdddocPanel//\\", $"{subGuid}-{FileName}");

                UrlPath = $"{subGuid}-{FileName}";
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
            }
        }
    }
}
