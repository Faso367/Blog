using System.ComponentModel.DataAnnotations;

namespace VideoService.ViewModels
{
    public class CommentViewModel
    {
        //[Required]
        public int PostId { get; set; }
        //[Required]
        public int MainCommentId { get; set; }
        //[Required]
        public string Message { get; set; }

        public string Author { get; set; }

        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }

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
