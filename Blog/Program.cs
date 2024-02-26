using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blog.Configuration;
using Blog.Data;
using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Services.Email;
using Blog.Controllers;

//using Umbraco.Web;

public class Program
{
    public static void Main(string[] args)
    {

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            // ��������� �������� ApplicationContext � �������� ������� � ���������� (����� ���������� � MS SQL)
            IServiceCollection services = builder.Services;

            // <AppDbContext> - ������ <> ��� ������ (�� ���� ��� ��� �������)
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SmtpSettings");

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // �������������� �������� ������ SmtpSettings, ��������������� ���������� �� appsettings.json
            services.Configure<SmtpSettings>(config);


            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // ��������� ����������� �� ������ � ������ (����� IdentityUser ��� �������� ���� ��������/�����/��������/������ � ���������)
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            // ������ ��������� �� ���������, ����� �������� ���������� � ������ (��� �� ����������� 1 �����, ��������� �����...)
            {
                options.Password.RequireDigit = true; // �����
                options.Password.RequireNonAlphanumeric = false; // ����������
                options.Password.RequireUppercase = true; // ��������� �����
                options.Password.RequiredLength = 8; // �����

            }).AddEntityFrameworkStores<AppDbContext>();

            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
            });

            // ���������, ��� ��� ������ � �� ������������ ����� ��������� � �����, ������� ��������� ���
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddSingleton<IEmailService, EmailService>();
            //services.AddSingleton<PostController>();

            services.AddMvc(options =>
            {

                options.EnableEndpointRouting = false;
                // ������ ���������� ����� ����������� ��� �� �����
                // (CSS + JS �����, � ����������� ������������ ����������� ����� ���� ���������� (���� ��� ������� � �����������),
                // �� ���� ��� ����� ��������� ���-�� � ������� � �� ����� ������������ ������ ��� �������
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

