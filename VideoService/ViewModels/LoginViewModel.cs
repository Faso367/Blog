using System.ComponentModel.DataAnnotations;

namespace VideoService.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        //public string? UserName { get; set; }

        [DataType(DataType.Password)] //- обозначаем тип поля для всего приложения (даже в html оно будет заполняться точками, чтобы пароль не был виден)
        public string Password { get; set; }
        //public string? Password { get; set; }
    }
}
