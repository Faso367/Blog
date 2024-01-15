
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
    public class HomeController : Controller
    {
        private IFileManager _fileManager;
        private IRepository _repo;

        public HomeController(IRepository repo, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _repo = repo; // Получаем экземпляр интерфейса для доступа ко всем его методам
        }

        //public HomeController()
        //{

        //}

        public IActionResult Index()
        {
            return View();
        }

        //// ОБЪЯСНЕНИЕ МЕТОДА НИЖЕ
        //public IActionResult Index(int pageNumber, string category, string search)
        //{
        //    // Если пользователь хочет перейти на нулевую или отрицательную страницу,
        //    // то отправляем на первую
        //    if (pageNumber < 1)
        //    {
        //        return RedirectToAction("Index", new { pageNumber = 1, category });
        //    }

        //    // Наполняем IndexViewModel и передаем на вход представлению ее экземпляр

        //    var vm = _repo.GetAllPosts(pageNumber, category, search);

        //    return View(vm);
        //}

    }
}
