using MedClinic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models.Doctor
{
    public class DoctorDateViewModel
    {
        public DoctorModel Doctor { get; set; }
        public IEnumerable<PatientDataModel> DoctorDatas { get; set; }
    }
}
