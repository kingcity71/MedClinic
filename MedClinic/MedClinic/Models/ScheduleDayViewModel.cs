using MedClinic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models
{
    public class ScheduleDayViewModel
    {
        public DateTime Date { get; set; }
        public Guid SpecialiazationId { get; set; }
        public string Specialiazation { get; set; }
        public List<Schedule> ScheduleDay { get; set; }
    }
}
