using System;
using System.Collections;
using System.Collections.Generic;
using MedClinic.Model;
using MedClinic.Model.Patient;

namespace MedClinic.Interfaces
{
    public interface IPatientService
    {
        void CreatePatient(PatientModel patientModel);
        PatientModel GetPatient(Guid id);
        PatientModel GetPatient(String email);
        void UpdatePatient(PatientModel patientModel);
        IEnumerable<PatientDataModel> GetPatientData(Guid patientId);
        IEnumerable<PatientDataModel> GetPatientData(Guid patientId, Guid specId);
        IEnumerable<ConslusionModel> GetConclusions(Guid patientId);
        IEnumerable<ConslusionModel> GetConclusions(Guid patientId, Guid specId);
        IEnumerable<MyScheduleModel> GetMySchedules(Guid id);
    }
}
