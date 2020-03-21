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
            var patient = context.Patients.FirstOrDefault(x=>x.Id==id);
            var patientModel = MapPatientModel(patient);
            return patientModel;
        }

        public IEnumerable<PatientDataModel> GetPatientData(Guid patientId)
        {
            var patientData = context.PatientDatas
                .Where(x => x.PatientId == patientId).ToList();
            var properties = context.Properties.ToList();
            var patientDataModels = patientData.Select(x => new PatientDataModel()
            {
                Date=x.Date,
                PropName = properties.FirstOrDefault(y=>y.Id==x.PropertyId)?.Name,
                PropValue = x.Value
            }).ToList();

            return patientDataModels;
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
