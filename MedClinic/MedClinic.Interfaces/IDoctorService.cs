using System;
using System.Collections.Generic;
using MedClinic.Model;

namespace MedClinic.Interfaces
{
    public interface IDoctorService
    {
        void CreateDoctor(DoctorModel doctorModel);
        DoctorModel GetDoctor(Guid id);
        DoctorModel GetDoctor(string email);
        Dictionary<string, Guid> GetSpecializations();
        void UpdateDoctor(DoctorModel doctorModel);

        void OpenClosedSchedules(DateTime date, Guid doctorId);
        void CloseOpenedSchedules(DateTime date, Guid doctorId);
    }
}
