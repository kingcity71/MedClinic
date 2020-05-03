using AutoMapper;
using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using MedClinic.Model;
using MedClinic.Model.Patient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedClinic.Services
{
    public class PatientService : IPatientService
    {
        private readonly MedClinicContext context;
        private readonly IDoctorService doctorService;

        public PatientService(MedClinicContext context, IDoctorService doctorService)
        {
            this.context = context;
            this.doctorService = doctorService;
        }
        public void CreatePatient(PatientModel patientModel)
        {
            var patient = MapPatientModel(patientModel);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        public IEnumerable<ConslusionModel> GetConclusions(Guid patientId)
        {
            var conclusions = context.Conclusions
                .Where(x => x.PatientId == patientId)
                .OrderBy(x => x.Date)
                .ToList();

            var patientConclusionsModels = conclusions.Select(x => new ConslusionModel()
            {
                Date = x.Date,
                Doctor = doctorService.GetDoctor(x.DoctorId),
                Result = x.Result
            }).ToList();

            return patientConclusionsModels;
        }

        public PatientModel GetPatient(Guid id)
        {
            var patient = context.Patients.FirstOrDefault(x => x.Id == id);
            var patientModel = MapPatientModel(patient);
            return patientModel;
        }
        public PatientModel GetPatient(string email)
        {
            var patient = context.Patients.FirstOrDefault(x => x.Email == email);
            var patientModel = MapPatientModel(patient);
            return patientModel;
        }

        public IEnumerable<PatientDataModel> GetPatientData(Guid patientId)
        {
            var patientData = context.PatientDatas
                .Where(x => x.PatientId == patientId)
                .OrderBy(x => x.Date)
                .ToList();
            var properties = context.Properties.ToList();
            var patientDataModels = patientData.Select(x => new PatientDataModel()
            {
                Date = x.Date,
                PropName = properties.FirstOrDefault(y => y.Id == x.PropertyId)?.Name,
                PropValue = x.Value
            }).ToList();

            return patientDataModels;
        }
        public void UpdatePatient(PatientModel patientModel)
        {
            var patient = context.Patients.FirstOrDefault(x => x.Id == patientModel.Id);
            if (patient == null) return;
            patient.FullName = patientModel.FullName;
            patient.MedData = patientModel.MedData;
            patient.PassData = patientModel.PassData;
            patient.Photo = patientModel.Photo == null && patient.Photo != null
                ? patient.Photo : patientModel.Photo;
            patient.Sex = patientModel.IsMan == true;
            context.Patients.Update(patient);
            context.SaveChanges();
        }

        public IEnumerable<MyScheduleModel> GetMySchedules(Guid id)
        {
            var schedules = context.Schedules.Where(x => x.PatientId == id).ToList();
            var schedulesModel = schedules
                .Select(x => new MyScheduleModel()
                {
                    ScheduleId = x.Id,
                    Place = x.Place,
                    DateTime = x.Date,
                    Doctor = doctorService.GetDoctor(x.DoctorId),
                    Specialization = doctorService.GetDoctor(x.DoctorId).Specialization,
                    Status = x.Status
                })
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return schedulesModel;
        }

        PatientModel MapPatientModel(Patient patient)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Patient, PatientModel>()
            .ForMember("IsMan", opt => opt.MapFrom(p => p.Sex == true))
            .ForMember("IsWoman", opt => opt.MapFrom(p => p.Sex == false)));
            var mapper = new Mapper(config);
            var patientModel = mapper.Map<PatientModel>(patient);
            return patientModel;
        }

        Patient MapPatientModel(PatientModel patientModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PatientModel, Patient>()
                .ForMember("Sex", opt => opt.MapFrom(p => p.IsMan == true)));

            var mapper = new Mapper(config);
            var patient = mapper.Map<Patient>(patientModel);
            return patient;
        }
    }
}
