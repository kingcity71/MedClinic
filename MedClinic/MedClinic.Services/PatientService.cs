using MedClinic.Data;
using MedClinic.Interfaces;
using MedClinic.Model;
using System;

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
            throw new NotImplementedException();
        }

        public void UpdatePatient(PatientModel patientModel)
        {
            throw new NotImplementedException();
        }
    }
}
