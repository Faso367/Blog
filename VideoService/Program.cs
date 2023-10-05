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
        /*var builder = WebApplication.CreateBuilder(
            new WebApplicationOptions()
            {
                WebRootPath = "wwwroot"
            }
        );*/

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            //var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SmtpSettings")["Videoservice"];

            // ������ ��������� ��
            //string connection = "Server=(localdb)\\MSSQLLocalDB;Database=MyBlog;Trusted_Connection=True;";

            // ���������
            //"DefaultConnection": "Server=DESKTOP-IF0N1RI;Database=MyBlog;User=Lekaic_777;Password=gajopar745;TrustServerCertificate=True;Encrypt=False"

            // ������ �������
            //"DefaultConnection": "Server=DESKTOP-IF0N1RI;Database=MyBlog;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False"

            // ������� ��
            //string connection = "Data Source=SQL6031.site4now.net;Initial Catalog=db_a9d119_lekaic777;User Id=db_a9d119_lekaic777_admin;Password=gajopar745;";

            // ��������� �������� ApplicationContext � �������� ������� � ���������� (����� ���������� � MS SQL)
            IServiceCollection services = builder.Services;

            // <AppDbContext> - ������ <> ��� ������ (�� ���� ��� ��� �������)
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SmtpSettings");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // �������������� �������� ������ SmtpSettings, ��������������� ���������� �� appsettings.json
            services.Configure<SmtpSettings>(config);

            // ��������� ����������� �� ������ � ������ (����� IdentityUser ��� �������� ���� ��������/�����/��������/������ � ���������)
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            //services.AddDefaultIdentity<IdentityUser>(options =>
            // ������ ��������� �� ���������, ����� �������� ���������� � ������ (��� �� ����������� 1 �����, ��������� �����...)
            {
                options.Password.RequireDigit = false; // �����
                options.Password.RequireNonAlphanumeric = false; // ����������
                options.Password.RequireUppercase = false; // ��������� �����
                options.Password.RequiredLength = 6; // �����

            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
            });

            /*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Auth/Login";
            options.AccessDeniedPath = "/accessdenied";
        });*/


            // ���������, ��� ��� ������ � �� ������������ ����� ��������� � �����, ������� ��������� ���
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddSingleton<IEmailService, EmailService>();

            services.AddMvc(options =>
            {

                options.EnableEndpointRouting = false;
                // ������ ���������� ����� ����������� ��� �� �����
                // (CSS + JS �����, � ����������� ������������ ����������� ����� ���� ���������� (���� ��� ������� � �����������),
                // �� ���� ��� ����� ��������� ���-�� � ������� � �� ����� ������������ ������ ��� �������
                options.CacheProfiles.Add("Monthly", new Microsoft.AspNetCore.Mvc.CacheProfile { Duration = 60 * 60 * 24 * 7 * 4 });
            }
            );


            var app = builder.Build();

            var scope = app.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            // ��������� ���� ��������������
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

                // ��� ����� ������ ����� ��������, ���� ������� ��� �������
                userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

            }

            app.UseStaticFiles();
            //app.UseAuthorization();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            // ��� ����������� ������ �����
            app.UseDeveloperExceptionPage();
            // ��� ����������� ������ HTTP
            app.UseStatusCodePages();
            app.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

