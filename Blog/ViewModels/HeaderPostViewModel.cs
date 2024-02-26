namespace Blog.ViewModels
{
    public class HeaderPostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = " "; // поле для хранения заголовка записи
        public string Author { get; set; } = " "; // создатель поста
    }
}
