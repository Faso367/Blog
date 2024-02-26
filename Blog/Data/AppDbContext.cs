using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Blog.Models;
using Blog.Models.Comments;

// Здесь мы определяем таблицы (ячейки - это свойства классов), которые будут в БД

namespace Blog.Data
{
    // наследуемся от IdentityDbContext, чтобы было проще работать с полями БД для авторизации
    // IdentityDbContext содержит встроенные методы для авторизации
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } //Database.EnsureCreated(); } 

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        //    modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        //    modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();

        //    modelBuilder.Entity<MainComment>()
        //    .HasMany(e => e.AuthorReactions)
        //    .WithOne(e => e.MainComment)
        //    .HasForeignKey(e => e.MainCommentId)
        //    .IsRequired();

        //    modelBuilder.Entity<ExistenseAuthorReaction>().HasNoKey();

        //    //modelBuilder.Entity<MainComment>()
        //    //.HasMany(c => c.AuthorReactions)
        //    //.WithOne(o => o.MainComment);
        //    //.WithRequired(o => o.MainComment);

        //    //HasOne(b => b.Customer).WithOne(c => c.BankAccount).HasForeignKey<BankAccount>(f => f.Id);


        //    //modelBuilder.Entity<ExistenseAuthorReaction>()
        //    //    .HasOne(u => u.Comment)
        //    //    .WithMany(c => c.AuthorReactions)
        //    //    .HasForeignKey(u => u.I);
        //    //modelBuilder.Entity<ExistenseAuthorReaction>().HasRequired<Comment>(p => p.Comment).WithMany(t => t.Players).HasForeignKey(p => p.TeamId);

        //}



        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { Database.EnsureCreated(); } 

        // Определяем таблицу Posts в нашей БД
        public DbSet<Post> Posts { get; set; }

        public DbSet<ExistenseAuthorReaction> AuthorReactions { get; set; }

        // Для основных и доп комментов будут создаваться отдельные таблицы
        public DbSet<MainComment> MainComments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }

    }
}
