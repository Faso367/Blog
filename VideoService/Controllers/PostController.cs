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
        //[HttpGet]
        //public ActionResult changeReactionsCount(int postId, int mainCommentId, bool like, bool increment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Получаем имя текущего пользователя
        //        string? currentUserName = User.Identity?.Name;

        //        //Console.WriteLine($"{postId} {mainCommentId} {like}");

        //        var post = _repo.GetPost(postId);

        //        if (post != null && !currentUserName.IsNullOrEmpty())
        //        {

        //            MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

        //            if (mainComment != null)
        //            {
        //                if (increment)
        //                {
        //                    if (like) mainComment.LikesCount++;
        //                    else mainComment.DislikesCount++;
        //                }
        //                else
        //                {
        //                    if (like) mainComment.LikesCount--;
        //                    else mainComment.DislikesCount--;
        //                }

        //                _repo.UpdatePost(post);

        //                if (_repo.SaveChanges())
        //                    return new JsonResult("true");
        //            }
        //        }
        //    }
        //    return new JsonResult("false");
        //}

        [HttpPost]
        public void changeReactionsCount(int postId, int mainCommentId, bool like, bool increment)
        {
            if (ModelState.IsValid)
            {
                //Получаем имя текущего пользователя
                string? currentUserName = User.Identity?.Name;

                //Console.WriteLine($"{postId} {mainCommentId} {like}");

                var post = _repo.GetPost(postId);

                if (post != null && !currentUserName.IsNullOrEmpty())
                {

                    MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

                    if (mainComment != null)
                    {
                        if (increment)
                        {
                            if (like) mainComment.LikesCount++;
                            else mainComment.DislikesCount++;
                        }
                        else
                        {
                            if (like) mainComment.LikesCount--;
                            else mainComment.DislikesCount--;
                        }

                        _repo.UpdatePost(post);

                        _repo.SaveChanges();
                        //    return new JsonResult("true");
                    }
                }
            }
            //return new JsonResult("false");
        }


        /*
        [HttpGet]
        public ActionResult Decrement(int postId, int mainCommentId, bool like)
        {
            //Получаем имя текущего пользователя
            string? currentUserName = User.Identity?.Name;

            Console.WriteLine($"{postId} {mainCommentId} {like}");

            var post = _repo.GetPost(postId);

            if (post != null)
            {

                MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

                if (mainComment != null && !currentUserName.IsNullOrEmpty())
                {
                    if (A(mainComment, currentUserName, like)) {... };

                    //else
                    //    mainComment.DislikesCount++;

                    _repo.UpdatePost(post);
                    //await _repo.SaveChangesAsync();

                    if (_repo.SaveChanges())
                        return new JsonResult("true");
                    //if (_repo.SaveChangesAsync()) Console.WriteLine(777);
                }
            }
            return new JsonResult("false");
        }*/

        //private bool MatchingWithDbIsSuccess(MainComment mainComment, string userName, bool like)

        //{
        //    // Если нажали на лайк
        //    if (like == true) {

        //        // Если лайк уже был нажат
        //        if (WasLiked(mainComment, userName))
        //            mainComment.LikesCount--;
        //        else
        //            mainComment.LikesCount++;

        //        // Если до этого был нажат дизлайк
        //        if(WasDisliked(mainComment, userName))
        //            mainComment.DislikesCount--;
        //    }

        //    // Если нажали на дизлайк
        //    if (like == false)
        //    {
        //        // Если дизлайк уже был нажат
        //        if (WasDisliked(mainComment, userName))
        //            mainComment.DislikesCount--;
        //        else
        //            mainComment.DislikesCount++;

        //        // Если до этого был нажат лайк
        //        if (WasLiked(mainComment, userName))
        //            mainComment.LikesCount--;
        //    }

        //}


        //private static bool WasLiked(MainComment mainComment, string userName) =>
        //    WasAppreciated(mainComment, userName, "like")[0];

        //private static bool WasDisliked(MainComment mainComment, string userName) =>
        //    WasAppreciated(mainComment, userName, "like")[1];

        ///// <summary>
        ///// Определяет был ли лайкнут и дизлайкнут пост этим пользователем
        ///// </summary>
        ///// <param name="mainComment"></param>
        ///// <param name="userName"></param>
        ///// <param name="reactionName"></param>
        ///// <returns></returns>
        //private static bool[] WasAppreciated(MainComment mainComment, string userName, string reactionName)
        //{
        //    // Под первым элементом массива хранится ответ на вопрос: этот пост был лайкнут?
        //    // Под вторым элементом массива хранится ответ на вопрос: этот пост был дизлайкнут?
        //    var result = new bool[2] { false, false };

        //    foreach (KeyValuePair<string, Dictionary<string, bool>> dict in mainComment.AuthorsAndLikeExistence)
        //        // Если пользователь реагировал на пост
        //        if (dict.Key == userName)
        //            foreach (KeyValuePair<string, bool> pair in dict.Value)
        //            {
        //                if (pair.Key == reactionName)
        //                    result[0] = (pair.Value == true) ? true : false;

        //                else if (pair.Key == reactionName)
        //                    result[1] = (pair.Value == true) ? true : false;
        //            }
        //    return result;
        //}

    }
}
