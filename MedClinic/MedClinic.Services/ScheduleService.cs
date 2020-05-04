using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using MedClinic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedClinic.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly MedClinicContext context;
        private readonly IDoctorService doctorService;

        public ScheduleService(MedClinicContext context, IDoctorService doctorService)
        {
            this.context = context;
            this.doctorService = doctorService;
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
        public List<ScheduleTimeModel> GetTimeSchedule(DateTime dateTime, Guid specializationId)
        {
            var doctors = context
                .Doctors
                .Where(x => x.SpecializationId == specializationId)
                .Select(x => x.Id);

            var specialization = context
                .Specializations
                .FirstOrDefault(x => x.Id == specializationId);

            var timeSchedules = context.Schedules
                .Where(x => x.Date.Date == dateTime.Date 
                && x.Date.Hour == dateTime.Hour
                && doctors.Contains(x.DoctorId))
                .OrderBy(x => x.Date.Hour)
                .ToList();

            return timeSchedules
                .Select(x => new ScheduleTimeModel()
                {
                    ScheduleId = x.Id,
                    Status = x.Status,
                    Place = x.Place,
                    DateTime = dateTime,
                    Doctor = doctorService.GetDoctor(x.DoctorId),
                    SpecializationId = specializationId,
                    SpecializationName = specialization.Name
                })
                .ToList();
        }
    
        public void MakeAppointment(Guid schedId, Guid patientId)
        {
            var sched = context.Schedules.FirstOrDefault(x => x.Id == schedId);
            sched.PatientId = patientId;
            sched.Status = "Запись";
            context.SaveChanges();
        }

        public void CancelAppointment(Guid schedId)
        {
            var sched = context.Schedules.FirstOrDefault(x => x.Id == schedId);
            sched.PatientId = Guid.Empty;
            sched.Status = "Открыт";
            context.SaveChanges();
        }

        public List<Schedule> GetDoctorDaySchedule(DateTime date, Guid doctorId)
        {
            var schedule = context.Schedules
                .Where(x => x.DoctorId == doctorId && x.Date.Date == date.Date)
                .OrderByDescending(x => x.Date)
                .ToList();
            return schedule;
        }

        public Schedule GetSchedule(Guid id) => context.Schedules.FirstOrDefault(x=>x.Id==id);

        public void UpdateAppointment(ScheduleTimeModel model)
        {
            var sched = context.Schedules.FirstOrDefault(x => x.Id == model.ScheduleId);
            if (sched == null)
            {
                sched = new Schedule()
                {
                    Id = Guid.NewGuid(),
                    DoctorId = model.Doctor.Id
                };
                sched.Place = model.Place;
                sched.Status = model.Status;
                sched.Date = model.DateTime;
                sched.PatientId = Guid.Empty;
                context.Schedules.Add(sched);
                context.SaveChanges();
                return;
            }
                
            sched.Place = model.Place;
            sched.Status = model.Status;
            sched.Date = model.DateTime;
            if (model.Status == "Открыт")
                sched.PatientId = Guid.Empty;
            context.SaveChanges();
        }

        public void RemoveAppointment(Guid schedId)
        {
            var sched = context.Schedules.FirstOrDefault(x => x.Id == schedId);
            if (sched == null) return;
            context.Schedules.Remove(sched);
            context.SaveChanges();
        }

        public bool IsAppointmentExist(DateTime date, Guid doctorId)
            => context.Schedules.Any(x => x.DoctorId == doctorId && x.Date == date);
        public bool IsTimeToMakeAppointment(Guid patientId, DateTime date)
            => date.Date >= DateTime.Today
            && !context.Schedules.Any(x => x.PatientId == patientId && date == x.Date);

    }
}
