using System;
using System.Collections.Generic;
using System.Text;

namespace MedClinic.Model.Patient
{
    public class MyScheduleModel
    {
        public DateTime DateTime { get; set; }
        public DoctorModel Doctor { get; set; }
        public string Place { get; set; }
        public Guid ScheduleId { get; set; }
        public string Specialization { get; set; }
        public string Status { get; set; }
    }
}
