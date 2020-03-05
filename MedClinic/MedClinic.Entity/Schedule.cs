using System;

namespace MedClinic.Entity
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string Place { get; set; }
        public string Status { get; set; }
    }
}
