using Blog.Models.Comments;

namespace Blog.Models
{

    // Здесь мы определяем поля

    public class Post
    {
        //public int? Id { get; set; }
        public int Id { get; set; }

        public string Title { get; set; } = " "; // поле для хранения заголовка записи
        public string Body { get; set; } = " "; // поле для хранения тела записи
        public string Image { get; set; } = " "; // поле для хранения названия фото
        public string Author { get; set; } = " "; // создатель поста
        public string Description { get; set; } = " ";
        public string Tags { get; set; } = " ";
        public string Category { get; set; } = " ";
        public DateTime Created { get; set; } = DateTime.Now;
        // Пост должен содержать список первостепенных (основных комментов)
        // а уже у основных будут дополнительные (которые тоже могут быть основными для других дополнительных)
        public List<MainComment> MainComments { get; set; }
        //public List<MainComment>? MainComments { get; set; }
    }
}
