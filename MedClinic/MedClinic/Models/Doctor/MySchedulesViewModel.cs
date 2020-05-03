using MedClinic.Entity;
using MedClinic.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models.Doctor
{
    public class MySchedulesViewModel
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public DoctorModel Doctor { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
        public string Status { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
    }
}
