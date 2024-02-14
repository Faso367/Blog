
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using VideoService.Data;
using VideoService.Data.FileManager;
using VideoService.Data.Repository;
using VideoService.Models;
using VideoService.Models.Comments;
using VideoService.ViewModels;

namespace VideoService.Controllers
{
    // Наследуемся от класса Controller, реализованного в AspnetCore MVC
    public class FeedController : Controller
    {
        private IFileManager _fileManager;
        private IRepository _repo;

        public FeedController(IRepository repo, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _repo = repo; // Получаем экземпляр интерфейса для доступа ко всем его методам
        }

        // ОБЪЯСНЕНИЕ МЕТОДА НИЖЕ
        public IActionResult Index(int pageNumber, string category, string search)
        {
            // Если пользователь хочет перейти на нулевую или отрицательную страницу,
            // то отправляем на первую
            if (pageNumber < 1)
            {
                return RedirectToAction("Index", new { pageNumber = 1, category });
            }

            // Наполняем IndexViewModel и передаем на вход представлению ее экземпляр

            var vm = _repo.GetAllPosts(pageNumber, category, search);

            return View(vm);
        }

        //public IActionResult Post(int postId) =>
        //    View(_repo.GetPost(postId));
        public IActionResult Post(int postId)
        {
            var s = _repo.GetPost(postId);
            return View(s);
        }

        [HttpGet("/Image/{image}")]
        // Указываем, что изображения будут кешироваться по указанной схеме
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult Image(string image) =>
            new FileStreamResult(
                _fileManager.ImageStream(image),
                $"image/{image.Substring(image.LastIndexOf('.') + 1)}");


        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            //vm.Author = User.Identity.Name;


            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { postId = vm.PostId });

            //if(!User.Identity.IsAuthenticated)
            //{

            //}

            var post = _repo.GetPost(vm.PostId);

            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();
                
                //string s = FindTimeDifferenceAndCovertToWords(mainComment.Created);



                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now,
                    Author = vm.Author,
                    LikesCount = vm.LikesCount,
                    DislikesCount = vm.DislikesCount,
                    //AuthorReactions = new List<ExistenseAuthorReaction> { new ExistenseAuthorReaction {} }
                    
                    
                    //AuthorsAndLikeExistence = vm.AuthorsAndLikeExistence
                });

                

                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                    Author = vm.Author,
                    LikesCount = vm.LikesCount, // ???????
                    DislikesCount = vm.DislikesCount // ???????

                    //AuthorsAndLikeExistence = vm.AuthorsAndLikeExistence
                };
                _repo.AddSubComment(comment);
            }

            await _repo.SaveChangesAsync();

            // return RedirectToAction("Post", new { id = vm.PostId }); // БЫЛО
            post.Id = vm.PostId;

            //return RedirectToAction("Post", post);
            return RedirectToAction("Post", new { postId = vm.PostId });
        }

        //private string FindTimeDifferenceAndCovertToWords(DateTime postCreatedTime)
        //{
        //    string result = "";

        //    TimeSpan timeDifference = DateTime.Now - postCreatedTime;

        //    //string strTimeDifference = timeDifference.ToString();

        //    //TimeSpan time = TimeSpan.Parse(strTimeDifference.Substring(0, 10));

        //    double totalDays = timeDifference.TotalDays;
        //    double totalHours = timeDifference.TotalHours;
        //    double totalMinutes = timeDifference.TotalMinutes;

        //    //double days = timeDifference.Days;
        //    //double hours = timeDifference.Hours;
        //    //double minutes = timeDifference.Minutes;

        //    if (totalDays > 0)
        //    {
        //        if (totalDays > 364)
        //            result = CreateCorrectNumeral(totalDays / 365, "year");
        //        else
        //            result = CreateCorrectNumeral(totalDays, "day");

        //    }
        //    else if (totalHours > 0)
        //    {
        //        result = CreateCorrectNumeral(totalHours, "hour");
        //    }
        //    else if (totalMinutes > 0)
        //    {
        //        result = CreateCorrectNumeral(totalHours, "minute");
        //    }
        //    else
        //    {
        //        result = "меньше минуты назад";
        //    }

        //    Console.WriteLine(result);

        //    return result;

        //    string CreateCorrectNumeral(double time, string type)
        //    {

        //        var timeUnits_defaultValues = new Dictionary<string, string[]> {
        //            {"minute", new string[] { "минуту", "минуты", "минут" }},
        //            {"hour", new string[] { "час", "часа", "часов" }},
        //            {"day", new string[] { "день", "дня", "дней" }},
        //            {"year", new string[] { "год", "года", "лет" }}
        //        };

        //        if (timeUnits_defaultValues.ContainsKey(type))
        //        {
        //            string strTime = time.ToString();

        //            foreach (var unit in timeUnits_defaultValues.Keys)
        //                switch (strTime[^1] == '1' && strTime != "11" ? 1 :
        //                         strTime[^1] == '2' || strTime[^1] == '3' || strTime[^1] == '4' ? 2 : 3)
        //                {
        //                    case 1: return strTime + timeUnits_defaultValues[unit][0] + " назад";
        //                    case 2: return strTime + timeUnits_defaultValues[unit][1] + " назад";
        //                    case 3: return strTime + timeUnits_defaultValues[unit][2] + " назад";
        //                }
        //        }
        //        return "";
        //    }


            //// Если это like, то true. Если dislike, то false
            //[HttpGet]
            //public void Increment(int postId, int mainCommentId, bool like)
            //{
            //    var post = _repo.GetPost(postId);
            //    MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

            //    if (mainComment != null)
            //    {
            //        if (like)
            //            mainComment.LikesCount++;
            //        else
            //            mainComment.DislikesCount++;
            //        _repo.UpdatePost(post);
            //    }
            //    //else
            //    //{

            //    //}
            //    //int likesCount = post.MainComments.FirstOrDefault()?.LikesCount ?? 0;


            //}


            // ТЕ ЖЕ МЕТОДЫ, НО В ИМПЕРАТИВНОМ ПРЕДСТАВЛЕНИИ

            // Метод из интерфейса соответствует названию нашего отображения (Index.cshtml)
            // 1) public IActionResult Index(string category)
            //{
            //    //_repo.GetAllPosts(); // Получаем список значений ячеек БД
            //    var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts() :
            //        _repo.GetAllPosts(category);
            //    // то же самое что и
            //    /*
            //    if (string.IsNullOrEmpty(category)) {
            //    _repo.GetAllPosts();
            //    }
            //    else { _repo.GetAllPosts(category); }
            //    */
            //    return View(posts);
            //}

            // 2) public IActionResult Post(int id)
            //{
            //    var post = _repo.GetPost(id);

            //    return View(post);
            //}

            //[HttpGet("/Image/{image}")]
            // 3) public IActionResult Image(string image)
            //{
            //    var extension = image.Substring(image.LastIndexOf('.') + 1);
            //    return new FileStreamResult(_fileManager.ImageStream(image), $"image/{extension}");
            //}
        }
}
