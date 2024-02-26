using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models.Comments;
using Blog.ViewModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Blog.Controllers
{
    public class PostController : Controller
    {


        private IRepository _repo;
        private IFileManager _fileManager;
        //private int _postId;
        //private AppDbContext _context;
        //private Post _post;
        //private ExistenseAuthorReaction _authorReaction;

        public PostController(IRepository repo, IFileManager fileManager)
        {
            // Создаем экземпляр интерфейса, чтобы пользоваться его методами
            _repo = repo;
            _fileManager = fileManager;
            //_context = context;
            //_postId = context.Model.ModelId;
            //_postId = pvm.Id;
        }

        //[HttpGet]
        //public void GetDataFromView(int postId)
        //{
        //    _postId = postId;
        //    Console.WriteLine(_postId);
        //    //return View();
        //}

        // !!!!!!!!!!!!!!!!!!!!!!!!!!
        //private readonly AppDbContext _context;

        //public PostController(AppDbContext context )
        //{
        //    _context = context;

        //}

        // GET: Post
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Posts.ToListAsync());
        //}

        //// GET: Post/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.Posts
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(post);
        //}

        //// GET: Post/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Post/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Body,Image,Author,Description,Tags,Category,Created")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(post);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(post);
        //}

        //// GET: Post/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.Posts.FindAsync(id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(post);
        //}

        //// POST: Post/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Image,Author,Description,Tags,Category,Created")] Post post)
        //{
        //    if (id != post.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(post);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PostExists(post.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(post);
        //}

        //// GET: Post/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.Posts
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(post);
        //}

        //// POST: Post/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var post = await _context.Posts.FindAsync(id);
        //    if (post != null)
        //    {
        //        _context.Posts.Remove(post);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PostExists(int id)
        //{
        //    return _context.Posts.Any(e => e.Id == id);
        //}
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            public IActionResult Index(int postId)
            {
                var post = _repo.GetPost(postId);
                //_postId = postId;
                //_postId = postId;
                return View(post);
            }

            [HttpPost]
            public async Task<IActionResult> Comment(CommentViewModel vm)
            {
                //vm.Author = User.Identity.Name;


                if (!ModelState.IsValid)
                    // Запрашиваем страницу снова
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

                        //AuthorReactions = vm.AuthorReactions // !!!!!!!!!!!!!!!!!!!!!!

                        AuthorReactions = new List<ExistenseAuthorReaction> {
                        new ExistenseAuthorReaction {
                        ReactionAuthor = "",
                        LikeReaction = false,
                        DislikeReaction = false
                        }
                    }
                        //AuthorReactions = vm.AuthorReactions

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
            //return RedirectToAction("Post", new { postId = vm.PostId });
            return RedirectToAction("Index", new { postId = vm.PostId });
        }

            /// <summary>
            /// Меняет количество лайков или дизлайков, для безопасности логика из js продублирована.
            /// Не позволяет одному и тому же пользователя лайкнуть один комментарий дважды.
            /// </summary>
            /// <param name="postId"></param>
            /// <param name="mainCommentId"></param>
            /// <param name="like"></param>
            /// <param name="increment"></param>
            [HttpPost]
            public void changeReactionsCount(int postId, int mainCommentId, bool like)
            {

                Console.WriteLine(like == true ? "true" : "false");

                /*Надо сделать проверку на то, делался ли лайк до этого */

                if (ModelState.IsValid)
                {
                    //Получаем имя текущего пользователя
                    string? currentUserName = User.Identity?.Name;

                    var post = _repo.GetPost(postId);

                    if (post != null && !currentUserName.IsNullOrEmpty())
                    {

                        MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);
                        int mainCommentIndex = post.MainComments.IndexOf(mainComment);
                        //var i = mainComment.

                        if (mainComment != null)
                        {
                            // Реакицй нет, тк мы их не добавляли. При реагировании на пост надо пополнять список AuthorReactions
                            foreach (var reaction in mainComment.AuthorReactions)
                            {
                                //if (false && false)
                                if ((reaction.LikeReaction == false && reaction.DislikeReaction == false)
                                    || (reaction.ReactionAuthor == currentUserName)) // !!!!!!!!!!!!!!!!!! Просто не выполняется
                                {
                                    // Переменная нужна, чтобы прога не заходила в else блок после if блока
                                    //int reactionIndex = mainComment.AuthorReactions.IndexOf(reaction);
                                    var likeReaction = reaction.LikeReaction;
                                    var dislikeReaction = reaction.DislikeReaction;

                                    //if (reaction.ReactionAuthor == currentUserName)
                                    //{
                                    //    if (reaction.LikeReaction == true) { }
                                    //}

                                    // Инкремент
                                    // if (increment)
                                    //{
                                    // Если нажали на лайк
                                    if (like == true)
                                    {

                                        // Если лайк уже был нажат
                                        if (likeReaction == true)
                                        {
                                            // !!!!!!!!!!!!!!!!
                                            //if (reaction.ReactionAuthor != currentUserName)
                                            mainComment.LikesCount--;

                                            reaction.LikeReaction = false;
                                        }

                                        else
                                        {
                                            // !!!!!!!!!!!!!!!!
                                            //if (reaction.ReactionAuthor != currentUserName)
                                            mainComment.LikesCount++;

                                            reaction.LikeReaction = true;
                                        }

                                        // Если до этого был нажат дизлайк
                                        if (dislikeReaction == true)
                                        {
                                            // !!!!!!!!!!!!!!!!
                                            //if (reaction.ReactionAuthor != currentUserName)
                                            mainComment.DislikesCount--;

                                            reaction.DislikeReaction = false;
                                        }
                                        reaction.ReactionAuthor = currentUserName;
                                    }

                                    // Если нажали на дизлайк
                                    if (like == false)
                                    {
                                        // Если дизлайк уже был нажат
                                        if (dislikeReaction == true)
                                        {
                                            // !!!!!!!!!!!!!!!!
                                            //if (reaction.ReactionAuthor != currentUserName)
                                            mainComment.DislikesCount--;

                                            reaction.DislikeReaction = false;
                                        }
                                        else
                                        {
                                            // !!!!!!!!!!!!!!!!
                                            //if (reaction.ReactionAuthor != currentUserName)
                                            mainComment.DislikesCount++;

                                            reaction.DislikeReaction = true;
                                        }

                                        // Если до этого был нажат лайк
                                        if (likeReaction == true)
                                        {
                                            // !!!!!!!!!!!!!!!!
                                            //if (reaction.ReactionAuthor != currentUserName)
                                            mainComment.LikesCount--;

                                            reaction.LikeReaction = false;
                                        }
                                        reaction.ReactionAuthor = currentUserName;
                                    }

                                    //mainComment.AuthorReactions[reactionIndex] = reaction;
                                    post.MainComments[mainCommentIndex] = mainComment;

                                    _repo.UpdatePost(post);

                                    _repo.SaveChanges();
                                }
                            }// foreach закончился
                             //    return new JsonResult("true");
                        }
                    }
                }
            }

            [HttpGet]
            public ActionResult ShowTimeDifference(int postId, int mainCommentId, int subCommentId)
            {
                //Console.WriteLine($"postId: {_postId}");
                Console.WriteLine($"subCommentId: {subCommentId}");


                if (ModelState.IsValid)
                {
                    //var post = _post;
                    // Раз время задаётся при создании поста и не меняется
                    var post = _repo.GetPost(postId);
                    if (post != null)
                    {
                        var mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

                        string s = (subCommentId == -1)
                            ? FindTimeDifferenceAndCovertToWords(mainComment.Created)
                            : FindTimeDifferenceAndCovertToWords(mainComment.SubComments.ToList().Find(x => x.Id == subCommentId).Created);

                        Console.WriteLine("Вывод: " + s);

                        if (mainComment != null)
                            return new JsonResult(s);
                    }
                }
                return new JsonResult("error");
            }

            //[HttpGet]
            //public ActionResult ShowTimeDifference(int postId, int mainCommentId, int subCommentId)
            //{
            //    Console.WriteLine(postId);

            //    if (ModelState.IsValid)
            //    {

            //        var post = _repo.GetPost(postId);
            //        if (post != null)
            //        {
            //            var mainComment = post.MainComments.Find(x => x.Id == mainCommentId);
            //            string s = FindTimeDifferenceAndCovertToWords(mainComment.Created);

            //            Console.WriteLine("Вывод: " + s);

            //            if (mainComment != null)
            //                return new JsonResult(s);
            //        }
            //    }
            //    return new JsonResult("error");
            //}

            private static string FindTimeDifferenceAndCovertToWords(DateTime postCreatedTime)
            {
                string result = "";

                TimeSpan timeDifference = DateTime.Now - postCreatedTime;

                //Func<double, int> DoubleToInt = Dtime => Convert.ToInt32(Math.Round(Dtime)) - 1;

                int totalDays = Convert.ToInt32(Math.Round(timeDifference.TotalDays)) - 1;
                int totalHours = Convert.ToInt32(Math.Round(timeDifference.TotalHours)) - 1;
                int totalMinutes = Convert.ToInt32(Math.Round(timeDifference.TotalMinutes)) - 1;

                if (totalDays > 0)
                {
                    if (totalDays > 364)
                        result = CreateCorrectNumeral(totalDays / 365, "year");
                    else if (totalDays > 30) // НЕ ВСЕГДА ПРАВИЛЬНО
                        result = CreateCorrectNumeral(totalDays / 30, "month");
                    else
                        result = CreateCorrectNumeral(totalDays, "day");

                }
                else if (totalHours > 0)
                    result = CreateCorrectNumeral(totalHours, "hour");

                else if (totalMinutes > 0)
                    result = CreateCorrectNumeral(totalMinutes, "minute");

                else
                    result = "меньше минуты назад";

                return result;

                string CreateCorrectNumeral(int time, string type)
                {

                    var timeUnits_defaultValues = new Dictionary<string, string[]> {
                    {"minute", new string[] { "минуту", "минуты", "минут" }},
                    {"hour", new string[] { "час", "часа", "часов" }},
                    {"day", new string[] { "день", "дня", "дней" }},
                    {"month", new string[] { "месяц", "месяца", "месяцев" }},
                    {"year", new string[] { "год", "года", "лет" }}
                };

                    if (timeUnits_defaultValues.ContainsKey(type))
                    {
                        string strTime = time.ToString();

                        foreach (var unit in timeUnits_defaultValues.Keys)
                            if (unit == type)
                                switch (strTime[^1] == '1' && strTime != "11" ? 1 :
                                        strTime[^1] == '2' || strTime[^1] == '3' || strTime[^1] == '4' ? 2 : 3)
                                {
                                    case 1: return $"{strTime} {timeUnits_defaultValues[unit][0]} назад";
                                    case 2: return $"{strTime} {timeUnits_defaultValues[unit][1]} назад";
                                    case 3: return $"{strTime} {timeUnits_defaultValues[unit][2]} назад";
                                };
                    }
                    return "";
                }
            }
        


    /*        string NeedChange(double time, string type)
            {
                //List<string> timeUnits = new List<string> { "minute", "hour", " day", "year" };

                var timeUnits_defaultValues = new Dictionary<string, string[]> {
                    {"minute", new string[] {"минуту", "минуты", "минут" } },
                    {"hour", new string[] {"час", "часа", "часов" } },
                    {"day", new string[] {"день", "дня", "дней" }},
                    {"year", new string[] {"год", "года", "лет" }}
                };

                //string[] unitsOfTime = [""];

                if (timeUnits_defaultValues.ContainsKey(type)) {

                    //foreach(var pai)

                    string strTime = time.ToString();

                    foreach (var unit in timeUnits_defaultValues.Keys)
                        switch ( strTime[^1] == '1' && strTime != "11" ? 1 :
                                 strTime[^1] == '2' || strTime[^1] == '3' || strTime[^1] == '4' ? 2 : 3 )
                        {
                                 //strTime == "11" ? 3 : 4 ) {

                            case 1: return timeUnits_defaultValues[unit][0];
                            case 2: return timeUnits_defaultValues[unit][1];
                            case 3: return timeUnits_defaultValues[unit][2];
                            //case 4: return timeUnits_defaultValues[unit][2];
                        }


                }
            }*/

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
}
}
