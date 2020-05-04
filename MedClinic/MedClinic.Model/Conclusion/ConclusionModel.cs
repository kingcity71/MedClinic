using System;

namespace MedClinic.Model.Conclusion
{
    public class ConclusionModel
    {
        public Guid Id { get; set; }
        public DoctorModel Doctor { get; set; }
        public PatientModel Patient { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
        public Guid ScheduleId { get; set; }
    }
}
