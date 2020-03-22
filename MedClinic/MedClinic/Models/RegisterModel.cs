using System;
using System.ComponentModel.DataAnnotations;

namespace MedClinic.Models
{
    public class RegisterModel
    {
        public bool IsDoctor { get; set; }
        [Required(ErrorMessage = "Не указано ФИО")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
