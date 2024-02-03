using System.ComponentModel.DataAnnotations;

namespace VideoService.ViewModels
{
    public class CommentViewModel
    {
        //[Required]
        public int PostId { get; set; }
        //[Required]
        public int MainCommentId { get; set; } = 0;
        //[Required]
        public string Message { get; set; }

        public string Author { get; set; }

        public int LikesCount { get; set; } = 0;
        public int DislikesCount { get; set; } = 0;

        //public int LikesCount { get; set; }
        //public int DislikesCount { get; set; } 

        //public Dictionary<string, bool[]> AuthorsAndLikeExistence { get; set; }

        //public Dictionary<string, Dictionary<string, bool>> AuthorsAndLikeExistence { get; set; }

        //public int LikesCount { get; set; } = 0;

        //public int DislikesCount { get; set; } = 0;

        /*        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        public string Name { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Description { get; set; }*/

    }
}
