using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoService.Configuration;
using VideoService.Data;
using VideoService.Data.FileManager;
using VideoService.Data.Repository;
using VideoService.Services.Email;

public class Program
{
    public static void Main(string[] args)
    {

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            // Добавляем контекст ApplicationContext в качестве сервиса в приложение (чтобы обращаться к MS SQL)
            IServiceCollection services = builder.Services;

            // <AppDbContext> - внутри <> имя класса (мы сами его так назвали)
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SmtpSettings");

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Инициализируем свойства класса SmtpSettings, соответствующие параметрам из appsettings.json
            services.Configure<SmtpSettings>(config);

            // добавляем авторизацию по логину и паролю (класс IdentityUser уже содержит поля телефона/почты/никнейма/пароля и валидацию)
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            //services.AddDefaultIdentity<IdentityUser>(options =>
            // Меняем настройки по умолчанию, чтобы изменить требования к паролю (там не обязательно 1 цифра, заглавная буква...)
            {
                options.Password.RequireDigit = false; // цифра
                options.Password.RequireNonAlphanumeric = false; // спецсимвол
                options.Password.RequireUppercase = false; // заглавная буква
                options.Password.RequiredLength = 6; // длина

            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
            });

            // Указываем, что для работы с БД используется такой интерфейс и класс, который реализует его
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddSingleton<IEmailService, EmailService>();

            services.AddMvc(options =>
            {

                options.EnableEndpointRouting = false;
                // Теперь ежемесячно будет обновляться кеш на сайте
                // (CSS + JS файлы, и динамически подгружаемые изображения могут быть кешированы (если это указать в контроллере),
                // то есть они будут храниться где-то у клиента и не будут подгружаться каждый раз сначала
                options.CacheProfiles.Add("Monthly", new Microsoft.AspNetCore.Mvc.CacheProfile { Duration = 60 * 60 * 24 * 7 * 4 });
            }
            );

            services.AddRazorPages().AddRazorRuntimeCompilation();

            var app = builder.Build();

            var scope = app.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();

            // Добавляем роль администратора
            var adminRole = new IdentityRole("Admin");
            var authRole = new IdentityRole("AuthUser");
            if (!ctx.Roles.Any())
            {
                roleMgr.CreateAsync(authRole).GetAwaiter().GetResult();
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                // create a role
            }
            if (!ctx.Users.Any(u => u.UserName == "admin"))
            {
                // create a role
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com",
                };


                string RoleId = adminRole.Id;
                string UserId = adminUser.Id;

                new IdentityUserRole<string>
                {
                    RoleId = RoleId,
                    UserId = UserId
                };

                var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                // ТУТ НУЖНО ДЕЛАТЬ ТОЧКУ ОСТАНОВА, ЕСЛИ СОЗДАЕМ ЭТО ВПЕРВЫЕ
                userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

            }

            app.UseStaticFiles();
            //app.UseAuthorization();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            // Для отображения ошибок проги
            app.UseDeveloperExceptionPage();
            // Для отображения ошибок HTTP
            app.UseStatusCodePages();
            app.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

