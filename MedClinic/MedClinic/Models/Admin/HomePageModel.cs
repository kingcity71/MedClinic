using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models.Admin
{
    public class HomePageModel
    {
        public string Key { get; set; }
        public Dictionary<Guid,string> Patients { get; set; }
        public Dictionary<Guid, string> Doctors { get; set; }
    }
}
