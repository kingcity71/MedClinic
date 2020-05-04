using MedClinic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models.Doctor
{
    public class AppointmentResultViewModel
    {
        public Guid SchedId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public PatientModel Patient { get; set; }
        public Guid ConclusionId { get; set; }
        public IEnumerable<PatientDataModel> PatientDatas { get; set; }
    }
}
