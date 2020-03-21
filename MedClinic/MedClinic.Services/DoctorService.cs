using AutoMapper;
using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using MedClinic.Model;
using System;
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
        public void CreateDoctor(DoctorModel patientModel)
        {
            throw new NotImplementedException();
        }

        public DoctorModel GetDoctor(Guid id)
        {
            var doctor = context.Doctors.FirstOrDefault(x => x.Id == id);
            var doctorModel = MapDoctorModel(doctor);
            return doctorModel;
        }

        public void UpdateDoctor(DoctorModel patientModel)
        {
            throw new NotImplementedException();
        }
        DoctorModel MapDoctorModel(Doctor doctor)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Doctor, DoctorModel>());
            var mapper = new Mapper(config);
            var doctorModel = mapper.Map<DoctorModel>(doctor);
            doctorModel.Specialization = context.Specializations
                .FirstOrDefault(x => x.Id == doctor.SpecializationId)?.Name;
            return doctorModel;
        }
    }
}
