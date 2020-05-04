using MedClinic.Models.Attr;
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
        [DataType(DataType.Date, ErrorMessage = "Введите дату в формате ДД.ММ.ГГГГ")]
        [DateMin(ErrorMessage = "Дата должна быть больше 1920.01.01")]
        public DateTime BirthDate { get; set; }

        [EmailAddress(ErrorMessage ="Введите Email в формате name@domain.domain")]
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
