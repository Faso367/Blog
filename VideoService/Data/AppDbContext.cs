using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoService.Models;
using VideoService.Models.Comments;

// Здесь мы определяем таблицы, которые будут в БД

namespace VideoService.Data
{
    // наследуемся от IdentityDbContext, чтобы было проще работать с полями БД для авторизации
    // IdentityDbContext содержит встроенные методы для авторизации
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 

        // Определяем таблицу Posts в нашей БД
        public DbSet<Post> Posts { get; set; }

        // Для основных и доп комментов будут создаваться отдельные таблицы
        public DbSet<MainComment> MainComments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }

    }
}
