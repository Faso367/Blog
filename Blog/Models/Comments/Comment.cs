using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog.Models.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }

        // Имя пользователя, который создал комментарий
        public string Author { get; set; }

        // Имя пользователя, который отреагировал на комментарий
        //public string ReactionAuthor { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        //public virtual List<ExistenseAuthorReaction> AuthorReactions { get; set; } ??????????????
        // = new List<ExistenseAuthorReaction>
        //{
        //        new ExistenseAuthorReaction {
        //        ReactionAuthor = "",
        //        LikeReaction = false,
        //        DislikeReaction = false }
        //};

        //public List<> ReactionAuthor { get; set; }

        //public Dictionary<string, bool[]> AuthorsAndLikeExistence { get; set; }


        // Сопоставляет любого пользователя и наличие у него лайков/дизлайков этого комментария
        //public Dictionary<string, Dictionary<string, bool>> AuthorsAndLikeExistence { get; set; }

        public DateTime Created { get; set; }
    }

    // Нужен для проверки того, лайкал/дизлайкил ли автор комментарий раньше

    //public class ExistenseAuthorReaction : Comment
    //{
    //    //[Key]
    //    //public int CommentId { get; set; }

    //    //[ForeignKey("Id")]
    //    //public virtual Comment? Comment { get; set; }

    //    /// <summary>
    //    /// Внешний ключ
    //    /// </summary>
    //    //public int CommentId { get; set; }

    //    /// <summary>
    //    /// Навигационное свойство
    //    /// </summary>
    //    //public Comment? Comment { get; set; }

    //    /// <summary>
    //    /// Имя пользователя, который поставил лайк или дизлайк
    //    /// </summary>
    //    public string ReactionAuthor { get; set; }// = "";
    //    /// <summary>
    //    /// Пользователь лайкал комментарий до этого?
    //    /// </summary>
    //    public bool LikeReaction { get; set; }// = false;
    //    /// <summary>
    //    /// Пользователь дизлайкал комментарий до этого?
    //    /// </summary>
    //    public bool DislikeReaction { get; set; }// = false;

    //    //public ExistenseAuthorReaction ExistenseAuthorReaction { get; set; }
    //}
}
