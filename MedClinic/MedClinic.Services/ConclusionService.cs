using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using MedClinic.Model;
using MedClinic.Model.Conclusion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedClinic.Services
{
    public class ConclusionService : IConclusionService
    {
        private readonly MedClinicContext context;
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;

        public ConclusionService(MedClinicContext context,
            IDoctorService doctorService,
            IPatientService patientService)
        {
            this.context = context;
            this.doctorService = doctorService;
            this.patientService = patientService;
        }

        public ConclusionModel Create(ConclusionModel conclusionModel)
        {
            var conclusion = new Conclusion()
            {
                Id = Guid.NewGuid(),
                Date = conclusionModel.Date,
                DoctorId = conclusionModel.Doctor.Id,
                PatientId = conclusionModel.Patient.Id,
                Result = conclusionModel.Result,
                ScheduleId = conclusionModel.ScheduleId
            };
            conclusionModel.Id = conclusion.Id;
            context.Conclusions.Add(conclusion);
            context.SaveChanges();
            return conclusionModel;
        }

        public ConclusionModel GetConclusion(Guid id)
        {
            var conclusion = context.Conclusions.FirstOrDefault(x => x.Id == id);
            if (conclusion == null) return new ConclusionModel();
            var doctor = doctorService.GetDoctor(conclusion.DoctorId);
            var patient = patientService.GetPatient(conclusion.PatientId);
            return new ConclusionModel()
            {
                Date=conclusion.Date,
                Doctor=doctor,
                Result=conclusion.Result,
                Patient=patient,
                Id=conclusion.Id,
                ScheduleId=conclusion.ScheduleId
            };
        }

        public ConclusionModel GetConclusionBySched(Guid schedId)
        {
            var conclusion = context.Conclusions.FirstOrDefault(x => x.ScheduleId == schedId);
            if (conclusion == null) return new ConclusionModel();
            var doctor = doctorService.GetDoctor(conclusion.DoctorId);
            var patient = patientService.GetPatient(conclusion.PatientId);
            return new ConclusionModel()
            {
                ScheduleId = conclusion.ScheduleId,
                Date = conclusion.Date,
                Doctor = doctor,
                Result = conclusion.Result,
                Patient = patient,
                Id = conclusion.Id
            };
        }

       
        public void Remove(Guid id)
        {
            var conclusion = context.Conclusions.FirstOrDefault(x => x.Id == id);
            context.Conclusions.Remove(conclusion);
            context.SaveChanges();
        }

        public ConclusionModel Update(ConclusionModel conclusionModel)
        {
            var conclusion = context.Conclusions.FirstOrDefault(x => x.Id == conclusionModel.Id);
            conclusion.Result = conclusionModel.Result;
            conclusion.Date = conclusionModel.Date;
            context.SaveChanges();
            return conclusionModel;
        }
    }
}
