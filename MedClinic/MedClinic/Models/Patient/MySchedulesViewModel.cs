using System.Collections.Generic;
using MedClinic.Model;
using MedClinic.Model.Patient;
namespace MedClinic.Models.Patient
{
    public class MySchedulesViewModel
    {
        public PatientModel Patient{ get; set; }
        public IEnumerable<MyScheduleModel> Schedules{ get; set; }
    }
}
