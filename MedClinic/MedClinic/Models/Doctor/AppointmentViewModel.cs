using MedClinic.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedClinic.Models.Doctor
{
    public class AppointmentViewModel
    {
        public Guid ScheduleId { get; set; }
        public DateTime DateTime { get; set; }
        public PatientModel Patient { get; set; }
        
        [Required(ErrorMessage = "Укажите место")]
        public string Place { get; set; }
        
        [Required(ErrorMessage = "Укажите статус")]
        public string State { get; set; }
        
        public IEnumerable<SelectListItem> States { get; set; }
        [Required(ErrorMessage = "Укажите время")]
        public string Hour { get; set; }
        public IEnumerable<SelectListItem> Hours { get; set; }
    }
}
