using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VideoService.Data.FileManager;
using VideoService.Data.Repository;
using VideoService.Models;
using VideoService.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace VideoService.Controllers
{
    // к странице "панель администратора" имеет доступ только пользователь с ролью админа (admin) 


    //[Authorize]
    //[Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public PanelController(IRepository repo, IFileManager fileManager)
        {
            // Создаем экземпляр интерфейса, чтобы пользоваться его методами
            _repo = repo;
            _fileManager = fileManager;
        }
        

        public IActionResult Index()
        {
            // List<Post> posts  
            var posts = _repo.GetAllPosts(); // Получаем список значений ячеек БД
            return View(posts);
        }

        // По умолчанию используется метод Get для отображения View,
        // Если мы хотим использовать Post, то указываем [HttpPost]

        [HttpGet]
        // Метод для редактирования поста
        public IActionResult Edit(int? id)
        {

            // если такого идентификатора нет, то создаем экземпляр VM
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repo.GetPost((int)id);

                var ekz = new PostViewModel
                {
                    // Записываем в VM инфу, которую получили из БД по указанному id
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage = post.Image, // текущее изображение это последнее добавленное изображение
                    Description = post.Description,
                    Category = post.Category,
                    Tags = post.Tags,
                    //Author = post.Author // Мы же не меняем создателя поста, а только его содержимое
                };

                Console.WriteLine("EDIT ID_" + ekz.GetType().ToString());

                return View(ekz);


            }
        }


        [HttpPost]
        // Метод для создания или редактирования поста
        public async Task<IActionResult> Edit(PostViewModel vm)
        {

            //var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

            //// Если авторизация прошла с ошибкой
            //if (!result.Succeeded)
            //{
            //    // Возвращаем сообщение об ошибке от VM 
            //    return View(vm);
            //}

            //var a = User.Identity?.Name;

            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags,
                //Author = vm.Author
                Author = User.Identity.Name // Получаем имя авторизованного пользователя, который создал этот пост
        };

            Console.WriteLine("EDIT VM_" + post.GetType().ToString());

            if (vm.Image == null)
                post.Image = vm.CurrentImage;
            else
            {
                if (!string.IsNullOrEmpty(vm.CurrentImage))
                {
                    _fileManager.RemoveImage(vm.CurrentImage);
                }
                post.Image = await _fileManager.SaveImage(vm.Image);
            }

            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
            
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // Если изменения успешно сохранены, то переводим пользователя на страницу Index.html
            /*
            if (await _repo.SaveChangesAsync()) {
                Console.WriteLine("Go to Index");
                return RedirectToAction("Index");
            }
            // Иначе переводим пользователя на страницу Post.cshtml
            // Тут передается не тот объект, который ожидает представление, 
            //из-за этого ошибка
            else {
                Console.WriteLine("RETURN VIEW");
                return View(post);
            }*/

            // Переходим на страницу Index.cshtml
        }

        [HttpGet]
        // Метод для возврата представления
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UserProfile()
        {
            // Получаем список значений ячеек БД
               return View(_repo.GetAllUserPosts(User.Identity.Name)); // Получаем список значений ячеек БД
               //return posts;
               //var posts = _repo.GetAllPosts(); // Получаем список значений ячеек БД
            //return View(new PostViewModel());
        }

        //[HttpPost]
        //public IActionResult UserProfile(string author)
        //{
        //    // List<Post> posts  
        //    var posts = _repo.GetAllUserPosts("1"); // Получаем список значений ячеек БД
        //    //return posts;
        //    //var posts = _repo.GetAllPosts(); // Получаем список значений ячеек БД
        //    return View(posts);

        //}
    }
}
