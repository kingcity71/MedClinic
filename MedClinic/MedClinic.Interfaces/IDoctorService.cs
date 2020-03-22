using System;
using MedClinic.Model;

namespace MedClinic.Interfaces
{
    public interface IDoctorService
    {
        void CreateDoctor(DoctorModel patientModel);
        DoctorModel GetDoctor(Guid id);
        DoctorModel GetDoctor(string email);
        void UpdateDoctor(DoctorModel patientModel);
    }
}
