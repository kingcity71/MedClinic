using System;
using MedClinic.Model;

namespace MedClinic.Interfaces
{
    public interface IPatientService
    {
        void CreatePatient(PatientModel patientModel);
        PatientModel GetPatient(Guid id);
        void UpdatePatient(PatientModel patientModel);
    }
}
