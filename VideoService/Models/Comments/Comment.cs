namespace VideoService.Models.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public string Author { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public DateTime Created { get; set; }
    }
}
