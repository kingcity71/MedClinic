using System;

namespace MedClinic.Models.Patient
{
    public class AppointmentViewModel
    {
        public DateTime DateTime { get; set; }
        public string Doctor { get; set; }
        public string Specialization { get; set; }
        public string Place { get; set; }
        public Guid PatientId { get; set; }
        public Guid ScheduleId { get; set; }
    }
}
