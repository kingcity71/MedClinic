using MedClinic.Model;
using MedClinic.Models.Attr;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedClinic.Models.Doctor
{
    public class ConclusionViewModel
    {
        public Guid SchedId { get; set; }
        public Guid ConclusionId { get; set; }
        public PatientModel Patient{ get; set; }

        [Required(ErrorMessage = "Не указана дата заключения")]
        [DataType(DataType.Date, ErrorMessage = "Введите дату в формате ДД.ММ.ГГГГ")]
        [DateMin(ErrorMessage = "Дата должна быть больше 1920.01.01")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "Не указаны результаты")]
        public string Result { get; set; }
    }
}
