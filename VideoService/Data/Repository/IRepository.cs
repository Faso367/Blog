using VideoService.Models;
using VideoService.Models.Comments;
using VideoService.ViewModels;

namespace VideoService.Data.Repository
{
    //Теперь создав экземпляр интерфейса мы сможем использовать эти методы где угодно
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        //IEnumerable<int> PageNumbers(int pageNumber, int pageCount);
        FeedViewModel GetAllPosts(int pageNumber, string category, string search);
        IEnumerable<Post> GetAllUserPosts(string author);
        void AddPost(Post post); // метод для добавления записи в БД 
        void RemovePost(int id); // метод для удаления записи из БД по id
        void UpdatePost(Post post); // метод для перезаписи в БД

        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsync(); // Метод для сохранения изменений с БД с подтверждением
    }
}
