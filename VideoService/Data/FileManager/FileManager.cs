
using PhotoSauce.MagicScaler;

namespace VideoService.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private string? _imagePath;

        // используем IConfiguration, чтобы из файла appsettings.json вытащить путь к изображениям
        public FileManager(IConfiguration configuration)
        {
            _imagePath = configuration["Path:Images"];
        }

        public FileStream ImageStream(string image)
        {
            // Получаем из БД фото из каталога
            return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
        }

        public bool RemoveImage(string image)
        {
            try
            {
                var file = Path.Combine(_imagePath, image);
                if (File.Exists(file))
                    File.Delete(file);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
                // Лучше использовать Path.Combine, а не складывать куски путей в строковом представлении самому
                var save_path = Path.Combine(_imagePath);

                // Создаём папку для файла
                if (!Directory.Exists(save_path))
                {
                    Directory.CreateDirectory(save_path);
                }
                // Получаем подстроку после последней точки в названии изображения (получаем расширение файла)
                var extension = image.FileName.Substring(image.FileName.LastIndexOf('.'));

                // Сами создаем имя файла, чтобы на сайте не было видно полный путь к файлу (как на компе)
                // Создаём новое имя файла из текущего времени и расширения файла
                var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{extension}";

                // Создаём файловый поток, чтобы сохранить в БД файл, который мы получаем с сайта
                using (var fileStream = new FileStream(Path.Combine(save_path, fileName), FileMode.Create))
                {
                    MagicImageProcessor.ProcessImage(image.OpenReadStream(), fileStream, ImageOptions());

                }
                return fileName;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }

        // Создаем экземпляр класса из готовой библиотеки для обрезки фото
        // под наши параметры + файл начинает значительно меньше весить

        [Obsolete] //...
       
        private ProcessImageSettings ImageOptions()
        {

            return new ProcessImageSettings()
            {
                Width = 800,
                Height = 500,
                ResizeMode = CropScaleMode.Crop,
                SaveFormat = FileFormat.Jpeg,
                JpegQuality = 100,
                JpegSubsampleMode = ChromaSubsampleMode.Subsample420,
            };
        }
    }
}
