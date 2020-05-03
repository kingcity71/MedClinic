using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models
{
    public class AppointmentPatientViewModel
    {
        public DateTime DateTime { get; set; }
        public string Doctor { get; set; }
        public string Specialization { get; set; }
        public string Place { get; set; }
        public Guid PatientId { get; set; }
        public Guid ScheduleId { get; set; }
    }
}
