using MedClinic.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models.Doctor
{
    public class DoctorDateViewModel
    {
        public PatientModel Patient { get; set; }
        public DoctorModel Doctor { get; set; }
        public PatientDataModel DoctorDatas { get; set; }
        public IEnumerable<SelectListItem> Properties{ get; set; }
    }
}
