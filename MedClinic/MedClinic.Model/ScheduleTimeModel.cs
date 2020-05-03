using System;

namespace MedClinic.Model
{
    public class ScheduleTimeModel
    {
        public Guid ScheduleId { get; set; }
        public DoctorModel Doctor { get; set; }
        public DateTime DateTime { get; set; }
        public Guid SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public string Place { get; set; }
        public string Status { get; set; }
    }
}
