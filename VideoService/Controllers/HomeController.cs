
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
    public class HomeController : Controller
    {
        private IFileManager _fileManager;
        private IRepository _repo;

        public HomeController(IRepository repo, IFileManager fileManager)
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

        public IActionResult Post(int id) =>
            View(_repo.GetPost(id));


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
            
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });

                var post = _repo.GetPost(vm.PostId);

            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now,
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
                    
                };
                _repo.AddSubComment(comment);
            }

            await _repo.SaveChangesAsync();

            return RedirectToAction("Post", new { id = vm.PostId });
        }
    }
}
