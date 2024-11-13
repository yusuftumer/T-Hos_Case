namespace T_HosCase.Helpers
{
    public interface IFileUpload
    {
        string FileUploads(IFormFile file);
        string FileUploadsAdvert(IFormFile file);
        string FileUploadsPage(IFormFile file);
        string FileUploadsStaff(IFormFile file);
        string FileUploadsBlog(IFormFile file);
        string FileUploadsBanner(IFormFile file);
        string DeleteImage(string Name);
        string DeleteImages(List<string> images);
        string UpdateFile(IFormFile file, string Name);
        List<string> FileListUpload(List<IFormFile> files);
        string VideoUploads(IFormFile file);
        string DocumentUploads(IFormFile files);
    }
}
