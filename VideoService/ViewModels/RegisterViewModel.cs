using System.ComponentModel.DataAnnotations;

namespace VideoService.ViewModels
{
    public class RegisterViewModel
    {
        [Required] // Значит, что все поля обязательны для заполнения
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required] // Значит, что все поля обязательны для заполнения
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        // Если значения полей не равны, то вернем View с предупреждением
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    } 
}
