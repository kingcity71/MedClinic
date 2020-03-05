using AutoMapper;
using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using MedClinic.Model;
using System;
using System.Linq;

namespace MedClinic.Services
{
    public class PatientService : IPatientService
    {
        private readonly MedClinicContext context;

        public PatientService(MedClinicContext context)
        {
            this.context = context;
        }
        public void CreatePatient(PatientModel patientModel)
        {
            throw new NotImplementedException();
        }

        public PatientModel GetPatient(Guid id)
        {
            var patient = context.Patients.FirstOrDefault();
            var patientModel = MapPatientModel(patient);
            throw new NotImplementedException();
        }

        public void UpdatePatient(PatientModel patientModel)
        {
            throw new NotImplementedException();
        }

        PatientModel MapPatientModel(Patient patient)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Patient, PatientModel>()
            .ForMember("IsMan", opt=> opt.MapFrom(p=>p.Sex==true))
            .ForMember("IsWoman", opt => opt.MapFrom(p => p.Sex == false)));
            var mapper = new Mapper(config);
            var patientModel = mapper.Map<PatientModel>(patient);
            return patientModel;
        }
    }
}
