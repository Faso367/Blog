namespace VideoService.Models.Comments
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

        public List<ExistenseAuthorReaction> AuthorReactions { get; set; } = new();

        //public List<> ReactionAuthor { get; set; }

        //public Dictionary<string, bool[]> AuthorsAndLikeExistence { get; set; }


        // Сопоставляет любого пользователя и наличие у него лайков/дизлайков этого комментария
        //public Dictionary<string, Dictionary<string, bool>> AuthorsAndLikeExistence { get; set; }

        public DateTime Created { get; set; }
    }

    // Нужен для проверки того, лайкал/дизлайкил ли автор комментарий раньше

    public class ExistenseAuthorReaction
    {
        public int Id { get; set; }

        /// <summary>
        /// Внешний ключ
        /// </summary>
        //public int CommentId { get; set; }

        /// <summary>
        /// Навигационное свойство
        /// </summary>
        //public Comment? Comment { get; set; }

        /// <summary>
        /// Имя пользователя, который поставил лайк или дизлайк
        /// </summary>
        public string ReactionAuthor { get; set; } = "";
        /// <summary>
        /// Пользователь лайкал комментарий до этого?
        /// </summary>
        public bool LikeReaction { get; set; } = false;
        /// <summary>
        /// Пользователь дизлайкал комментарий до этого?
        /// </summary>
        public bool DislikeReaction { get; set; } = false;
    }
}
