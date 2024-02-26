using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class CreatePostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
