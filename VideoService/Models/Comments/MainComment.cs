namespace VideoService.Models.Comments
{
    public class MainComment : Comment
    {
        // Основной коммент содержит список всех дополнительных
        public List<SubComment> SubComments { get; set; }
    }
}
