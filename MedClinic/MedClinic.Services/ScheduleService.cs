using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedClinic.Services
{
    public class ScheduleService: IScheduleService
    {
        private readonly MedClinicContext context;
        public ScheduleService(MedClinicContext context)
        {
            this.context = context;
        }
        public List<Schedule> GetDaySchedule(DateTime date, Guid specializationId)
        {
            var doctors = context.Doctors.Where(x => x.SpecializationId == specializationId).Select(x => x.Id);
            var daySchedule = context.Schedules
                .Where(x => x.Date.Date == date.Date && doctors.Contains(x.DoctorId))
                .OrderBy(x => x.Date.Hour)
                .ToList();
            return daySchedule;
        }
    }
}
