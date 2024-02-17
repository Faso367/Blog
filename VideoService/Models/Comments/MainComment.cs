using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace VideoService.Models.Comments
{
    public class MainComment : Comment
    {


        //public virtual List<ExistenseAuthorReaction> AuthorReactions { get; set; }
        //public List<ExistenseAuthorReaction> AuthorReactions { get; set; }

        //public virtual ICollection<ExistenseAuthorReaction> AuthorReactions { get; } = new List<ExistenseAuthorReaction>();
        public virtual ICollection<ExistenseAuthorReaction> AuthorReactions { get; set; }
        // Основной коммент содержит список всех дополнительных
        //public List<SubComment> SubComments { get; set; }

        public virtual ICollection<SubComment> SubComments { get; set; }

    }
    
    public class ExistenseAuthorReaction
    {
        //[Key]
        public int Id { get; set; }

        //[ForeignKey("Id")]
        public virtual MainComment MainComment { get; set; } // !!!!!!!!!!!!!!!!!!!!!
        //public MainComment MainComment { get; set; }

        /// <summary>
        /// Обязательный внешний ключ
        /// </summary>
        public int MainCommentId { get; set; }
        /// <summary>
        /// Обязательное навигационное свойство
        /// </summary>
        //public virtual MainComment MainComment { get; set; }
        //public virtual MainComment MainComment { get; set; } = null!;

        /// <summary>
        /// Имя пользователя, который поставил лайк или дизлайк
        /// </summary>
        public string ReactionAuthor { get; set; }// = "";
        /// <summary>
        /// Пользователь лайкал комментарий до этого?
        /// </summary>
        public bool LikeReaction { get; set; }// = false;
        /// <summary>
        /// Пользователь дизлайкал комментарий до этого?
        /// </summary>
        public bool DislikeReaction { get; set; }// = false;

        //public ExistenseAuthorReaction ExistenseAuthorReaction { get; set; }
    }
}
