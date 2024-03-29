﻿//using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно для ввода")]
        //[ValidateName(ErrorMessage = "Name must not contain `zz`")]  // Значит, что все поля обязательны для заполнения
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required] // Значит, что все поля обязательны для заполнения
        [Required(ErrorMessage = "Это поле обязательно для ввода")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для ввода")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для ввода")]
        //[ValidatePa]
        [DataType(DataType.Password)]
        // Если значения полей не равны, то вернем View с предупреждением
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    } 
}
