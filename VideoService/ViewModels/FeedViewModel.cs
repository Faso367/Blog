using VideoService.Models;

namespace VideoService.ViewModels
{
    public class FeedViewModel
    {
        // Текущий номер страницы
        public int PageNumber { get; set; }

        // Количество страниц
        public int PageCount { get; set; }

        // Можем ли идти на след страницу?
        public bool NextPage { get; set; }

        public string Category { get; set; }

        public string Search { get; set; }

        public IEnumerable<int> Pages { get; set; }

        // Получаем список всех записей из таблицы БД
        public IEnumerable<Post> Posts { get; set; }

    }
}