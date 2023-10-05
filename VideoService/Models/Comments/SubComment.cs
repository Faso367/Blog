namespace VideoService.Models.Comments
{
    // Подкомментарии - ответы и другие комментарии к основному коменту (как на ютуб)
    public class SubComment : Comment
    {
        // Подкомментарий должен содержать Id основного, к которому принадлежит
        public int MainCommentId { get; set; }
    }
}
