using Microsoft.Extensions.Configuration;

namespace Blog.Configuration
{
    // Этот класс для входа на сервер по SMTP
    public class SmtpSettings
    {
        public string From{ get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
