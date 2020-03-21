using MedClinic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models
{
    public class PatientConclusionsViewModel
    {
        public PatientModel Patient { get; set; }
        public IEnumerable<ConslusionModel> Conslusions { get; set; }
    }
}
