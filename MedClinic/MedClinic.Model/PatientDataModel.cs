using System;
using System.Collections.Generic;
using System.Text;

namespace MedClinic.Model
{
    public class PatientDataModel
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid PropertyId { get; set; }
        public string PropName { get; set; }
        public string PropValue { get; set; }
        public DateTime Date { get; set; }
        public Guid ScheduleId { get; set; }
    }
}
