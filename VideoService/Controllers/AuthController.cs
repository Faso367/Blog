using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using VideoService.Services.Email;
using VideoService.ViewModels;


namespace VideoService.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        //public UserManager<IdentityUser> _userManager { get; private set; }
        private UserManager<IdentityUser> _userManager; // для управления пользователями используется не контекст данных, а класс - UserManager<T>
        private IEmailService _emailService;
        

        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Отвечаю Login.cshtml, передаю представлению на вход экземпляр LoginViewModel
            return View(new LoginViewModel());
        }
        

        [HttpPost]
        // Использую async Task, тк внутри крутится асинхронная функция
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            // Входим в систему/личный кабинет (авторизация)
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            //Флаг1, указывающий, должен ли файл cookie для входа сохраняться после закрытия браузера.
            //Флаг2, указывающий, должна ли учетная запись пользователя быть заблокирована в случае сбоя входа.

            // Если авторизация прошла с ошибкой
            if (!result.Succeeded)
            {
                // Возвращаем сообщение об ошибке от VM 
                return View(vm);   
            }
            // Получаем имя пользователя
            var user = await _userManager.FindByNameAsync(vm.UserName);
            // Это администратор?
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            //!!!!!!!!!!!!!!!!!!!!!
            if (isAdmin)
            {
                // Если это админ, то отправляем его на панель администратора
                return RedirectToAction("Index", "Panel");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            // привязываем модель-представление к представлению
            return View(new RegisterViewModel());
        }

        //[HttpPost]
        // Метод для сохранения данных, из формы для регистрации, в VM

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            // 04.01
            // Какого-то фига ConfirmedPassword передаётся = null

            //vm.PasswordHash = vm.Password.GetHashCode().ToString();

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // Заполняем в Model данные о новом пользователе
            var user = new IdentityUser
            {
                UserName = vm.Username,
                Email = vm.Email,
                //PasswordHash = vm.PasswordHash
            };
            // Сохраняем данные в БД
            //var result = await _userManager.CreateAsync(user, vm.PasswordHash); // НЕ ЗНАЮ ЛУЧШЕ С ПАРОЛЕМ ИЛИ С ХЕШЕМ ПАРОЛЯ

            var result = await _userManager.CreateAsync(user, vm.Password);

            await _userManager.AddToRoleAsync(user, "AuthUser"); // НЕ РАБОТАЕТ !!!!!!!!!!!!!!

            // Если данные в БД сохранились без ошибок
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                //await _emailService.SendEmail(user.Email, "Welcome", "Sir");
                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Выходим из системы
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }

}
