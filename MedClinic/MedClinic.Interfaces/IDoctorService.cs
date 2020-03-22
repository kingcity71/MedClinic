using System;
using System.Collections.Generic;
using MedClinic.Model;

namespace MedClinic.Interfaces
{
    public interface IDoctorService
    {
        void CreateDoctor(DoctorModel patientModel);
        DoctorModel GetDoctor(Guid id);
        DoctorModel GetDoctor(string email);
        Dictionary<string, Guid> GetSpecializations();
        void UpdateDoctor(DoctorModel patientModel);
    }
}
