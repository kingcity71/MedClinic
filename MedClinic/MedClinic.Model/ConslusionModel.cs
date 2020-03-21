using System;
using System.Collections.Generic;
using System.Text;

namespace MedClinic.Model
{
    public class ConslusionModel
    {
        public DoctorModel Doctor { get; set; }
        public PatientModel PatientModel { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
    }
}
