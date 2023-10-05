namespace VideoService.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = ""; // поле для хранения заголовка записи
        public string Body { get; set; } = ""; // поле для хранения тела записи
        public string Author { get; set; } = "";
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";
        public string CurrentImage { get; set; } = "";
        //IFormFile - интерфейс для хранения файлов любого формата (видео, фото, бинарник)
        public IFormFile Image { get; set; } = null; // поле для хранения 
    }
}
