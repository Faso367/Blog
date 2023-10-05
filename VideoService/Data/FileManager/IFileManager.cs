namespace VideoService.Data.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveImage(IFormFile image);
        FileStream ImageStream(string image);
        bool RemoveImage(string image);
    }
}
