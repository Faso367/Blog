namespace VideoService.Services.Email
{
    // Уже есть похожая реализация в IServiceCollection, но мы хотим создать сервис сами
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}
