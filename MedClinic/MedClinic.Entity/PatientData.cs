using System;

namespace MedClinic.Entity
{
    public class PatientData
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public string Value { get; set; }
        public Guid PatientId { get; set; }
        public DateTime Date { get; set; }
    }
}
