using System;
using System.Collections.Generic;
using System.Text;

namespace MedClinic.Entity
{
    public class Conclusion
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid ScheduleId { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
    }
}
