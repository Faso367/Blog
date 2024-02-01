using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VideoService.Data.FileManager;
using VideoService.Data.Repository;
using VideoService.Models.Comments;

namespace VideoService.Controllers
{
    public class PostController : Controller
    {
        private IRepository _repo;
        //private IFileManager _fileManager;

        public PostController(IRepository repo, IFileManager fileManager)
        {
            // Создаем экземпляр интерфейса, чтобы пользоваться его методами
            _repo = repo;
            //_fileManager = fileManager;
        }


        // Если это like, то true. Если dislike, то false
        //[HttpPost]
        //public async Task<int> Increment(int postId, int mainCommentId, bool like)
        [HttpGet]
        public ActionResult Increment(int postId, int mainCommentId, bool like)
        {
            //var user = User.
            // Получаем имя текущего пользователя
            //string? currentUser = User.Identity?.Name;

            //if (!currentUser.IsNullOrEmpty())
            //{

            //}

            Console.WriteLine($"{postId} {mainCommentId} {like}");

            var post = _repo.GetPost(postId);
            MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

            if (mainComment != null)
            {
                if (like)
                    mainComment.LikesCount++;
                else
                    mainComment.DislikesCount++;

                _repo.UpdatePost(post);
                //await _repo.SaveChangesAsync();

                if (_repo.SaveChanges())
                    return new JsonResult("true");
                //if (_repo.SaveChangesAsync()) Console.WriteLine(777);
            }

            return new JsonResult("false");
            //else // Если такой комментарий не найден, то возвращаю ноль
            //    return 0;

            //return like ? mainComment.LikesCount : mainComment.DislikesCount;

            // То же самое, что и
            // if (like) return mainComment.LikesCount;
            // else return mainComment.DislikesCount;
        }

        //[HttpPost]
        [HttpGet]
        public ActionResult Test(string name, int mainCommentId)
        {
            Console.WriteLine("\n" + name + " -------- " + mainCommentId.ToString() + " --------");
            return new JsonResult ("123");
        }


    }
}
