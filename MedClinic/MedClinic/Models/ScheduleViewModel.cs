using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models
{
    public class ScheduleViewModel
    {
        public DateTime Date { get; set; }
        public int[][] CalendarMatrix { get; set; }
        public Dictionary<string,Guid> Specializations { get; set; }
        public Guid SpecializationId { get; set; }
    }
}
