using System;
using MedClinic.Model;

namespace MedClinic.Interfaces
{
    public interface IDoctorService
    {
        void CreateDoctor(DoctorModel patientModel);
        DoctorModel GetDoctor(Guid id);
        void UpdateDoctor(DoctorModel patientModel);
    }
}
