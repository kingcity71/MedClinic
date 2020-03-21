using System.Collections.Generic;
using MedClinic.Model;

namespace MedClinic.Models
{
    public class PatientDataViewModel
    {
        public PatientModel Patient { get; set; }
        public IEnumerable<PatientDataModel> PatientDatas { get; set; }
    }
}
