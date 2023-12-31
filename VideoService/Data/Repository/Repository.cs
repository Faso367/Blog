﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using VideoService.Helpers;
using VideoService.Models;
using VideoService.Models.Comments;
using VideoService.ViewModels;

namespace VideoService.Data.Repository
{

    // Этот файл содержит список методов для работы с БД, тем самым абстрагирует наши вызовы (можем использовать эти методы где угодно при помощи интерфейса)

    public class Repository : IRepository
    {
        private AppDbContext _ctx;

        // ВСЕ НИЖЕ ПЕРЕЧИСЛЕННЫЕ МЕТОДЫ (кроме SaveChanges) по факту коммитят изменения,
        // БД изменяется только после вызова SaveChangesAsync()

        // Получаем контекст текущей сессии с БД
        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        // Добавляем запись в БД
        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post); // добавляем инфу в таблицу Posts

        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.ToList();
        }

        public IEnumerable<Post> GetAllUserPosts(string author)
        {
            //authorsPosts = _ctx.Posts.Where(x => x.Author == author);

            //return _ctx.Posts.ToList();

            //return _ctx.Posts.Where(x => x.Author == author);
            return _ctx.Posts.Where(x => x.Author == author);
        }

        //public IEnumerable<Post> GetAllPosts(string author)
        //{
        //    //authorsPosts = _ctx.Posts.Where(x => x.Author == author);

        //    //return _ctx.Posts.ToList();
        //    return _ctx.Posts.Where(x => x.Author == author);
        //}

        // Получаем все записи в БД
        public IndexViewModel GetAllPosts(int pageNumber,
            string category,
            string search)
        {
            // ToLower() - понижает регистр (применяем на всякий случай, чтобы не было ошибок
            // Вычисления на стороне клиента !
            Func<Post, bool> InCategory = (post) => {
                return post.Category.ToLower()
            .Equals(category.ToLower());
            };
            // !!!!!!!!!!!!!!!!!!!!!!!!!
            int pageSize = 2; // столько постов вмещает одна страница
            int skipAmount = pageSize * (pageNumber - 1); // размер сдвига
            //int postsCount = _ctx.Posts.Count(); // Получаем количество записей в таблице

            // Если всего записей больше, чем мы показали, то продолжаем увеличивать сдвиг и идем на след страницу

            // AsNoTracking() - позволяет сделать запрос в БД без отслеживания
            // То есть EF вытаскивает данные и не кеширует их (сразу применяет SaveChanges сам)
            //var query = _ctx.Posts.AsQueryable(); - УСТАРЕЛО 
            var query = _ctx.Posts.AsNoTracking().AsEnumerable();

            if (!String.IsNullOrEmpty(category))
                query = query.Where(x => InCategory(x));

            // Находим пост, если содержимое, заголовок или тема имеющегося поста
            // включает фразу из поисковой строки

            if (!String.IsNullOrEmpty(search))
                query = query.Where(x => x.Title.Contains(search)
                    || x.Body.Contains(search)
                    || x.Description.Contains(search));

            // Раньше это ускоряло получение данных из БД
            //if (!String.IsNullOrEmpty(search))
            //    query = query.Where(x => EF.Functions.Like(x.Title, $"%{search}%")
            //    || EF.Functions.Like(x.Body, $"%{search}%")
            //    || EF.Functions.Like(x.Description, $"%{search}%"));



            int postsCount = query.Count();
            // Делаем postsCount типа double (для однозначной работы Ceiling)
            // С помощью Ceiling округляем число в большую сторону
            // Преобразовываем для VM результат в int (тк метод вернет целое число типа double)
            int pageCount = (int)Math.Ceiling(((double)postsCount) / pageSize);

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount = pageCount,
                Pages = PageHelper.PageNumbers(pageNumber, pageCount).ToList(),
                NextPage = postsCount > skipAmount + pageSize,
                Category = category,
                Search = search,
                Posts = query
                .Skip(skipAmount) // Сдвигаем, чтобы не брать одни и те же записи для новой страницы
                .Take(pageSize) // Возвращаем 5 записей от начала сдвига из таблицы dbo.PostsToList()
                .ToList()
            };
        }

        // Получаем одну запись из БД
        public Post GetPost(int id)
        {
            // Если полученный id совпадает с найденным в БД, то возвращаем (достаем) эту запись
            return _ctx.Posts
                .Include(p => p.MainComments) // включаем в запись основные комментарии
                    .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }

        // Удаляем запись
        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        // Перезаписываем ячейку БД
        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }

        // 

        public async Task<bool> SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync(); // сохраняем изменения в БД

            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;

        }

        public void AddSubComment(SubComment comment)
        {
            _ctx.SubComments.Add(comment);
        }

    }
}
