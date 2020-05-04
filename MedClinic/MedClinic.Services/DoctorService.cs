using AutoMapper;
using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using MedClinic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MedClinic.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly MedClinicContext context;
        public DoctorService(MedClinicContext context)
        {
            this.context = context;
        }

        public IEnumerable<Schedule> GetSchedules(Guid doctorId, string status, int take, int skip)
        {
            var sched = status != null ?
                context.Schedules
                .Where(x => x.Status == status && x.DoctorId == doctorId)
                .OrderByDescending(x=>x.Date)
                .Skip(skip)
                .Take(take)
                .ToList()
                : context.Schedules
                .Where(x => x.DoctorId == doctorId)
                .OrderByDescending(x => x.Date)
                .Skip(skip)
                .Take(take)
                .ToList();
            return sched;
        }

        public int GetSchedulesCount(Guid doctorId, string status)
            => status != null ? context.Schedules.Count(x => x.DoctorId == doctorId && x.Status == status)
            : context.Schedules.Count(x => x.DoctorId == doctorId);

        public void CloseOpenedSchedules(DateTime date, Guid doctorId)
        {
            var scheds = context.Schedules
                .Where(x => x.Date.Date == date.Date && x.DoctorId == doctorId)
                .ToList();
            foreach (var sched in scheds)
                if (sched.Status == "Открыт")
                    context.Schedules.Remove(sched);

            context.SaveChanges();
        }

        public void CreateDoctor(DoctorModel doctorModel)
        {
            var doctor = MapDoctorModel(doctorModel);
            context.Doctors.Add(doctor);
            context.SaveChanges();
        }

        public DoctorModel GetDoctor(Guid id)
        {
            var doctor = context.Doctors.FirstOrDefault(x => x.Id == id);
            var doctorModel = MapDoctorModel(doctor);
            return doctorModel;
        }

        public DoctorModel GetDoctor(string email)
        {
            var doctor = context.Doctors.FirstOrDefault(x => x.Email == email);
            var doctorModel = MapDoctorModel(doctor);
            return doctorModel;
        }

        public Dictionary<string, Guid> GetSpecializations()
        {
            var dic = new Dictionary<string, Guid>();
            foreach (var spec in context.Specializations)
                dic.Add(spec.Name, spec.Id);
            return dic;
        }

        public void OpenClosedSchedules(DateTime date, Guid doctorId)
        {
            var doctor = GetDoctor(doctorId);
            var scheds = context.Schedules
                .Where(x => x.Date.Date == date.Date && x.DoctorId == doctorId)
                .ToList();

            for (var i = 10; i < 18; i++)
                if (!scheds.Any(x => x.Date.Hour == i))
                    context.Schedules.Add(new Schedule()
                    {
                        Date = new DateTime(date.Year, date.Month, date.Day, i, 0, 0),
                        Status = "Открыт",
                        Place = doctor.PlaceDefault,
                        DoctorId = doctorId,
                        PatientId = Guid.Empty
                    });

            context.SaveChanges();
        }

        public void UpdateDoctor(DoctorModel doctorModel)
        {
            var doctor = context.Doctors.FirstOrDefault(x => x.Id == doctorModel.Id);
            if (doctor == null) return;
            doctor.FullName = doctorModel.FullName;
            doctor.Education = doctorModel.Education;
            doctor.HireDate = doctorModel.HireDate;
            doctor.Photo = doctorModel.Photo == null && doctor.Photo != null
                ? doctor.Photo : doctorModel.Photo;
            doctor.SpecializationId = doctorModel.SpecializationId;
            doctor.PlaceDefault = doctorModel.PlaceDefault;
            context.Doctors.Update(doctor);
            context.SaveChanges();
        }

        DoctorModel MapDoctorModel(Doctor doctor)
        {
            if (doctor == null) return null;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Doctor, DoctorModel>());
            var mapper = new Mapper(config);
            var doctorModel = mapper.Map<DoctorModel>(doctor);
            doctorModel.Specialization = context.Specializations
                .FirstOrDefault(x => x.Id == doctor.SpecializationId)?.Name;
            return doctorModel;
        }
        Doctor MapDoctorModel(DoctorModel doctorModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DoctorModel, Doctor>());
            var mapper = new Mapper(config);
            var doctor = mapper.Map<Doctor>(doctorModel);
            return doctor;
        }

        public bool IsTherapist(Guid doctorId)
        {
            var specId = context.Specializations.FirstOrDefault(x=>x.Name.ToLower()=="терапевт")?.Id;
            return (context.Doctors.FirstOrDefault(x => x.Id == doctorId)?.SpecializationId == specId) == true;
        }
    }
}
