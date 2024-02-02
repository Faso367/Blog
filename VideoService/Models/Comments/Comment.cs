namespace VideoService.Models.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public string Author { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        //public Dictionary<string, bool[]> AuthorsAndLikeExistence { get; set; }


        // Сопоставляет любого пользователя и наличие у него лайков/дизлайков этого комментария
        //public Dictionary<string, Dictionary<string, bool>> AuthorsAndLikeExistence { get; set; }

        public DateTime Created { get; set; }
    }
}
